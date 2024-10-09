using Services.Abstractions;
using Services;
using Domain.Contracts.ISeeding;
using Domain.Contracts.IUnitOfWork;
using Persistence.Repositories;

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

            return services;
        }
    }
}
