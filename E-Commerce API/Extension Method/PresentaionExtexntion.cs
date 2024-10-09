using Domain.Contracts.ISeeding;
using Domain.Contracts.IUnitOfWork;
using E_Commerce_API.Middlewares;
using E_Commerce_API.Response_Factory;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repositories;

namespace E_Commerce_API.Extension_Method
{
    public static class PresentaionExtexntion
    {
        public static IServiceCollection PresentaionExtexntionServises(this IServiceCollection services)
        {

            // Add services to the container.

            services.AddControllers().AddApplicationPart(typeof(Presentation.PresentationRef).Assembly);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            // custum configuration

            services.Configure<ApiBehaviorOptions>(option =>

            {
                option.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidation;

            }

            );

            return services;
        }



       

    }

}
