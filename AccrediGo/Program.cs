using Microsoft.EntityFrameworkCore;
using AccrediGo.Infrastructure.Data;
using AccrediGo.Domain.Interfaces;
using AccrediGo.Infrastructure.Repositories;
using AccrediGo.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using AccrediGo.Application.Features.BillingDetails.SubscriptionPlans.CreateSubscriptionPlan;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AccrediGo.Models.Auth;
using AccrediGo.Services;
using AccrediGo.Application.Interfaces;
using AccrediGo.Application.Services;
using AccrediGo.Models.Common;
using AutoMapper;
using AccrediGo.Infrastructure;
using AccrediGo.Application.Common;

namespace AccrediGo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add logging with environment-specific configuration
            builder.Logging.AddConsole();
            builder.Logging.AddDebug();
            
            // Configure logging levels based on environment
            if (builder.Environment.IsDevelopment())
            {
                builder.Logging.SetMinimumLevel(LogLevel.Debug);
            }
            else if (builder.Environment.IsStaging())
            {
                builder.Logging.SetMinimumLevel(LogLevel.Information);
            }
            else
            {
                builder.Logging.SetMinimumLevel(LogLevel.Warning);
            }

            // Add services to the container.
            builder.Services.AddControllers();

            // Add CORS with environment-specific policies
            ConfigureCors(builder.Services, builder.Environment);

            // Register DbContext
            builder.Services.AddDbContext<AccrediGoDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            // Register AutoMapper - scan all assemblies for profiles
            builder.Services.AddAutoMapper(cfg => 
            {
                cfg.AddMaps(typeof(Program).Assembly);
                cfg.AddMaps(typeof(CreateSubscriptionPlanCommand).Assembly);
                cfg.AddProfile<AutoMapperProfile>();
            });

            // Register MediatR - scan all assemblies for handlers
            builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            
            // Configure JWT Authentication
            ConfigureJwtAuthentication(builder.Services, builder.Configuration);
            
            // Register JWT Service
            builder.Services.AddScoped<IJwtService, JwtService>();
            
            // Register CurrentRequest Service
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ICurrentRequest, CurrentRequest>();
            
            // Register Custom HttpContextInfo
            builder.Services.AddScoped<AccrediGo.Application.Services.IHttpContextInfo, AccrediGo.Infrastructure.HttpContextAccessor>();
            
            // Register Audit Service
            builder.Services.AddScoped<IAuditService, AuditService>();
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                // Configure OpenAPI document info with proper version (must be first)
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "AccrediGo API",
                    Version = "v1",
                    Description = "AccrediGo API for Accreditation Management System"
                });
                
                // Configure JWT Bearer token authentication
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\nExample: 'Bearer 12345abcdef'"
                });
                
                options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            var app = builder.Build();

            // Seed FacilityType and Accreditation tables if missing
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AccrediGoDbContext>();
                if (!db.FacilityTypes.Any())
                {
                    db.FacilityTypes.AddRange(new[]
                    {
                        new AccrediGo.Domain.Entities.MainComponents.FacilityType { TypeName = "Hospital" },
                        new AccrediGo.Domain.Entities.MainComponents.FacilityType { TypeName = "Clinic" },
                        new AccrediGo.Domain.Entities.MainComponents.FacilityType { TypeName = "Nursing Home" },
                        new AccrediGo.Domain.Entities.MainComponents.FacilityType { TypeName = "Laboratory" },
                        new AccrediGo.Domain.Entities.MainComponents.FacilityType { TypeName = "Pharmacy" }
                    });
                    db.SaveChanges();
                }

                if (!db.Accreditations.Any())
                {
                    db.Accreditations.AddRange(new[]
                    {
                        new AccrediGo.Domain.Entities.MainComponents.Accreditation { Id = "1", Name = "JCI" },
                        new AccrediGo.Domain.Entities.MainComponents.Accreditation { Id = "2", Name = "GAHAR" },
                        new AccrediGo.Domain.Entities.MainComponents.Accreditation { Id = "3", Name = "CBAHI" },
                        new AccrediGo.Domain.Entities.MainComponents.Accreditation { Id = "4", Name = "ISO 9001" },
                        new AccrediGo.Domain.Entities.MainComponents.Accreditation { Id = "5", Name = "NABH" },
                        new AccrediGo.Domain.Entities.MainComponents.Accreditation { Id = "6", Name = "CARF" },
                        new AccrediGo.Domain.Entities.MainComponents.Accreditation { Id = "7", Name = "Other" }
                    });
                    db.SaveChanges();
                }
            }

            // Configure the HTTP request pipeline based on environment
            ConfigurePipeline(app);

            app.Run();
        }

        private static void ConfigureCors(IServiceCollection services, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                // Development: Allow only frontend dev origin for security
                services.AddCors(options =>
                {
                    options.AddPolicy("DevelopmentPolicy", policy =>
                        policy.WithOrigins("http://localhost:3000")
                              .AllowAnyMethod()
                              .AllowAnyHeader()
                              .AllowCredentials());
                });
            }
            else if (environment.IsStaging())
            {
                // Staging: Allow specific origins for testing
                services.AddCors(options =>
                {
                    options.AddPolicy("StagingPolicy", policy =>
                        policy.WithOrigins("https://staging.accredigo.com", "https://test.accredigo.com")
                              .AllowAnyMethod()
                              .AllowAnyHeader()
                              .AllowCredentials());
                });
            }
            else
            {
                // Production: Restrict to specific production domains
                services.AddCors(options =>
                {
                    options.AddPolicy("ProductionPolicy", policy =>
                        policy.WithOrigins("https://accredigo.com", "https://www.accredigo.com")
                              .WithMethods("GET", "POST", "PUT", "DELETE")
                              .WithHeaders("Authorization", "Content-Type")
                              .AllowCredentials());
                });
            }
        }

        private static void ConfigureJwtAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            // Configure JWT Settings
            var jwtSettings = new JwtSettings();
            configuration.GetSection("JwtSettings").Bind(jwtSettings);
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            // Configure JWT Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SecretKey)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    RoleClaimType = System.Security.Claims.ClaimTypes.Role // Ensure role claim is mapped for [Authorize(Roles = ...)]
                };
            });

            // Configure Authorization
            services.AddAuthorization();
        }

        private static void ConfigurePipeline(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AccrediGo API v1");
                    c.RoutePrefix = "swagger";
                });
                // Development: More detailed error pages
                app.UseDeveloperExceptionPage();
                // Use development CORS policy BEFORE authentication/authorization
                app.UseCors("DevelopmentPolicy");
            }
            else if (app.Environment.IsStaging())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AccrediGo API v1");
                    c.RoutePrefix = "swagger";
                });
                // Staging: Custom error handling
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePages();
                // Use staging CORS policy BEFORE authentication/authorization
                app.UseCors("StagingPolicy");
            }
            else
            {
                // Production: Minimal error information for security
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePages();
                // Use production CORS policy BEFORE authentication/authorization
                app.UseCors("ProductionPolicy");
            }

            app.UseHttpsRedirection();
            // CORS must be before authentication/authorization
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}
