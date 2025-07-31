using Microsoft.EntityFrameworkCore;
using AccrediGo.Infrastructure.Data;

namespace AccrediGo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Retrieve environment variables
            //var sqlServerHost = Environment.GetEnvironmentVariable("SQLSERVER_HOST")
            //    ?? throw new InvalidOperationException("SQLSERVER_HOST environment variable is not set.");
            //var sqlServerDatabase = Environment.GetEnvironmentVariable("SQLSERVER_DATABASE")
            //    ?? throw new InvalidOperationException("SQLSERVER_DATABASE environment variable is not set.");
            //var sqlServerUsername = Environment.GetEnvironmentVariable("SQLSERVER_USERNAME")
            //    ?? throw new InvalidOperationException("SQLSERVER_USERNAME environment variable is not set.");
            //var sqlServerPassword = Environment.GetEnvironmentVariable("SQLSERVER_PASSWORD")
            //    ?? throw new InvalidOperationException("SQLSERVER_PASSWORD environment variable is not set.");

            // Build connection string
            //var connectionString = $"Server={sqlServerHost};Database={sqlServerDatabase};Integrated Security=True;TrustServerCertificate=True;";

            // Add services to the container.

            builder.Services.AddControllers();
            // Register DbContext
            builder.Services.AddDbContext<AccrediGoDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("AccrediGo.Infrastructure")
                )
            );
            builder.Services.AddScoped(typeof(AccrediGo.Domain.Interfaces.IGenericRepository<>),
                          typeof(AccrediGo.Infrastructure.Repositories.GenericRepository<>));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
