
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
using Microsoft.Extensions.Configuration;
using E_Commerce_API.Extension_Method;

namespace E_Commerce_API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // add PresentaionExtexntionServises

            builder.Services.PresentaionExtexntionServises();


            // add CoreProjectServises

            builder.Services.CoreProjectServises(builder.Configuration);

            // add InfrastructureProjectServises

            builder.Services.InfrastructureProjectServises(builder.Configuration);


         

            // Put  Logger in Console
            builder.Logging.AddConsole();


            #region Chain more than Services

            // add services IDbinitializer , we can chain to add more than Services 
            // builder.Services.AddScoped<IDbinitializer, Dbinitializer>().AddScoped<IUnitOfWork, UnitOfWork>();

            #endregion

            var app = builder.Build();

            #region pipelines

            await app.WebAppExtentionPipeLine();

            // Configure the HTTP request pipeline.

            app.CustomUseMiddlewareExtexntionServises();

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

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();



            app.Run();

            #endregion


        }



       
    }
}

