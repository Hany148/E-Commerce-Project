using Services.Abstractions;
using Services;
using Domain.Contracts.ISeeding;
using Domain.Contracts.IUnitOfWork;
using Persistence.Repositories;
using StackExchange.Redis;
using Persistence.Identity_DbContext;
using Domain.Idntity_Entities;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce_API.Extension_Method
{
    public static class InfrastructureProjectExtenstion
    {
        public static IServiceCollection InfrastructureProjectServises(this IServiceCollection services , IConfiguration configuration)
        {
            // Add Connection String for StoreContext
            services.AddDbContext<StoreContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("StoreContextConnection"))
            );

            // Add Connection String for StoreIdentityDbContext
            services.AddDbContext<StoreIdentityDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("StoreContextIdntityConnection"))
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

            services.ConfigureIdentityServises();

            return services;
        }
    
       public static IServiceCollection ConfigureIdentityServises (this IServiceCollection services)
        {

            services.AddIdentity<User, IdentityRole>
                (
                     option =>
                     {
                         option.Password.RequireNonAlphanumeric = false;
                         option.Password.RequireDigit = false;
                         option.Password.RequireLowercase = false;
                         option.Password.RequireUppercase = false;
                         option.Password.RequiredLength = 10;
                         option.User.RequireUniqueEmail = true; // defualt value is false
                     }

                ).AddEntityFrameworkStores<StoreIdentityDbContext>();

            return services;
        }

    }
}
