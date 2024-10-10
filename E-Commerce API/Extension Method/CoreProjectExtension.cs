using Services.Abstractions;
using Services;

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

            return services;
        }
    }
}
