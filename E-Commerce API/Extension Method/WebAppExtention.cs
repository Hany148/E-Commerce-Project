using Domain.Contracts.ISeeding;
using E_Commerce_API.Middlewares;

namespace E_Commerce_API.Extension_Method
{
    public static class WebAppExtention
    {
        public static async Task<WebApplication> WebAppExtentionPipeLine(this WebApplication app)
        {
            // 1. Create Type from type IDbinitializer
            using var scope = app.Services.CreateScope();

            var Dbinitializer = scope.ServiceProvider.GetRequiredService<IDbinitializer>();

            await Dbinitializer.InitializAsync();
            return app;
        }


        public static WebApplication CustomUseMiddlewareExtexntionServises(this WebApplication app)
        {


            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            return app;
        }

    }
}
