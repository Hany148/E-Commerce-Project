using Domain.Contracts.ISeeding;
using Domain.Contracts.IUnitOfWork;
using E_Commerce_API.Middlewares;
using E_Commerce_API.Response_Factory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
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
            services.SwaggerServices();


            // custum configuration

            services.Configure<ApiBehaviorOptions>(option =>

            {
                option.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidation;

            }

            );

            // AddCors

            services.AddCors(option =>
                    option.AddPolicy("CORSPolicy", bulider =>
                    {

                        bulider.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin(); // or => WithOrigins("http://localhost:4200")



                    })

            );

            return services;
        }


        public static IServiceCollection SwaggerServices(this IServiceCollection services)
        {

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(
                       option =>
                       {
                           option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                           {
                               In = ParameterLocation.Header,
                               Description = "Please Enter Bearer Token",
                               Name = "Authorization",
                               Type = SecuritySchemeType.Http,
                               Scheme = "Bearer",
                               BearerFormat = "JWT"

                           });


                           option.AddSecurityRequirement(new OpenApiSecurityRequirement
                           {
                               {
                                   new OpenApiSecurityScheme
                                   {
                                       Reference = new OpenApiReference
                                       {
                                           Type = ReferenceType.SecurityScheme,
                                           Id = "Bearer"
                                       }
                                   },
                                   new List<string>() {}
                               }
                           }

                           );



                       }

            );

            return services;
        }




    }

}
