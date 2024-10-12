using Services.Abstractions;
using Services;
using Domain.Contracts.ISeeding;
using Domain.Contracts.IUnitOfWork;
using Persistence.Repositories;
using StackExchange.Redis;

namespace E_Commerce_API.Extension_Method
{
    public static class InfrastructureProjectExtenstion
    {
        public static IServiceCollection InfrastructureProjectServises(this IServiceCollection services , IConfiguration configuration)
        {
            // Add Connection String for StoreContex
            services.AddDbContext<StoreContex>(options =>
            options.UseSqlServer(configuration.GetConnectionString("StoreContexConnection"))
            );

            // add services IDbinitializer 
            services.AddScoped<IDbinitializer, Dbinitializer>();

            // add services IUnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // add services IConnectionMultiplexer
            services.AddSingleton<IConnectionMultiplexer>(
                 
                     // service=> ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!)
                      _=> ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!)
                );


            return services;
        }
    }
}
