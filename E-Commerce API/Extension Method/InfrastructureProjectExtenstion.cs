using Services.Abstractions;
using Services;
using Domain.Contracts.ISeeding;
using Domain.Contracts.IUnitOfWork;
using Persistence.Repositories;
using StackExchange.Redis;
using Persistence.Identity_DbContext;
using Domain.Idntity_Entities;
using Microsoft.AspNetCore.Identity;
using Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace E_Commerce_API.Extension_Method
{
    public static class InfrastructureProjectExtenstion
    {
        public static IServiceCollection InfrastructureProjectServises(this IServiceCollection services, IConfiguration configuration)
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
                      _ => ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!)
                );


            // add services ConfigureIdentityServises

            services.ConfigureIdentityServises();


            // add services JWTServises

            services.JWTServises(configuration);

            return services;
        }

        public static IServiceCollection ConfigureIdentityServises(this IServiceCollection services)
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

        public static IServiceCollection JWTServises(this IServiceCollection services, IConfiguration configuration)
        {
            var options = configuration.GetSection("JWTOptions").Get<JWTOptions>();

            services.AddAuthentication(

                optionsAction =>
                {
                    optionsAction.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    optionsAction.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }

                ).AddJwtBearer(

                optionsValidation =>
                {
                    optionsValidation.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = options?.Issuer,
                        ValidAudience = options?.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options?.SecretKey!)),


                    };
                }

                );


            /*
            services.AddAuthorization(a =>
             a.AddPolicy("SuperAdmin", option =>

             {
                 option.RequireRole("SuperAdmin");
             }

             )

            );

            */

            return services;
        }

    }
}
