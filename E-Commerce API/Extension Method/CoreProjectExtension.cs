using Services.Abstractions;
using Services;
using Domain.Contracts___Interface__;
using Persistence.Repositories;

namespace E_Commerce_API.Extension_Method
{
    public static class CoreProjectExtension
    {
        public static IServiceCollection CoreProjectServises(this IServiceCollection services)
        {
            // add services IServiceManger
            services.AddScoped<IServiceManger, ServiceManger>();

            // add services AddAutoMapper
            services.AddAutoMapper(typeof(Services.ServicesRef).Assembly);


            // add services IBasketRepository
            services.AddScoped<IBasketRepository, BasketRepository>();

            return services;
        }
    }
}
