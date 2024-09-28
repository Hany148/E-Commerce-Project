
global using Domain.Contracts;
global using Microsoft.EntityFrameworkCore;
global using Persistence.Seeding.Class_Seeding;
global using presentence.Data.DbContexts;

namespace E_Commerce_API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add Connection String for StoreContex
            builder.Services.AddDbContext<StoreContex>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("StoreContexConnection"))
            );

            // add services
            builder.Services.AddScoped<IDbinitializer , Dbinitializer>();

            var app = builder.Build();

            await DbinitializerAsync(app);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

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
