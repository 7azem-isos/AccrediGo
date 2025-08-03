using Microsoft.EntityFrameworkCore;
using AccrediGo.Infrastructure.Data;
using AccrediGo.Domain.Interfaces;
using AccrediGo.Infrastructure.Repositories;
using AccrediGo.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using AccrediGo.Application.Mappers;
using AccrediGo.Application.Mappers.BillingDetails;
using AccrediGo.Application.Commands.BillingDetails.SubscriptionPlan.CreateSubscriptionPlan;

namespace AccrediGo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Register DbContext
            builder.Services.AddDbContext<AccrediGoDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped(typeof(IGenericQueryRepository<>), typeof(GenericQueryRepository<>));
            builder.Services.AddScoped(typeof(IGenericCommandRepository<>), typeof(GenericCommandRepository<>));
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());            // MediatR handlers
            builder.Services.AddMediatR(typeof(CreateSubscriptionPlanCommandHandler).Assembly);

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
