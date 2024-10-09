
global using Domain.Contracts;
global using Microsoft.EntityFrameworkCore;
global using Persistence.Seeding.Class_Seeding;
global using presentence.Data.DbContexts;
using Domain.Contracts.ISeeding;
using Presentation;
using Services.Abstractions;
using Services;
using Domain.Contracts.IUnitOfWork;
using Persistence.Repositories;
using Microsoft.Extensions.FileProviders;
using System;
using E_Commerce_API.Middlewares;
using Microsoft.AspNetCore.Mvc;
using E_Commerce_API.Response_Factory;

namespace E_Commerce_API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddApplicationPart(typeof(Presentation.PresentationRef).Assembly);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add Connection String for StoreContex
            builder.Services.AddDbContext<StoreContex>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("StoreContexConnection"))
            );

            // add services IDbinitializer
            builder.Services.AddScoped<IDbinitializer, Dbinitializer>();

            // add services IUnitOfWork
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // add services IServiceManger
            builder.Services.AddScoped<IServiceManger, ServiceManger>();

            // add services AddAutoMapper
            builder.Services.AddAutoMapper(typeof(Services.ServicesRef).Assembly);

            // custum configuration

            builder.Services.Configure<ApiBehaviorOptions>(option =>

            {
                option.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidation;

            }

            );


            var app = builder.Build();

            await DbinitializerAsync(app);

            // Configure the HTTP request pipeline.

            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            /* 
               app.UseStaticFiles(new StaticFileOptions(
                 FileProvider = new PhysicalFileProvider("put the Path of file")

                 ));
            */

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();



            app.Run();
        }

        public static async Task DbinitializerAsync(WebApplication app)
        {
            // 1. Create Type from type IDbinitializer
            using var scope = app.Services.CreateScope();

            var Dbinitializer = scope.ServiceProvider.GetRequiredService<IDbinitializer>();

            await Dbinitializer.InitializAsync();
        }
    }
}
