using Domain.Exceptions;
using Shared.ErrorModels;
using System.Net;

namespace E_Commerce_API.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public GlobalErrorHandlingMiddleware(ILogger logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext); // _next.Invoke(httpContext)

                if (httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
                {
                   await HandleNotFountPointAsync(httpContext);

                }

            }
            catch (Exception exception)
            {
                _logger.LogError($"Something Went Wrong {exception}");

                await HandleExceptionAsync(httpContext, exception);
            }
        }


        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            // 1. set status code of response to 500
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // 2. set content type of response to Application/json
            httpContext.Response.ContentType = "application/json";

            // 3. create standard response => return object of ErrorDetails as json
            var resonse = new ErrorDetails()
            {
                ErrorMessage = exception.Message,

            };

            // 4. change status code
            httpContext.Response.StatusCode = exception switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                AuthenticationException => (int)HttpStatusCode.Unauthorized,
                ValidationErrorsExpception errorsExpception => HandileValidationErrorsExpception(errorsExpception, resonse),
                _ => (int)HttpStatusCode.InternalServerError
            };

            resonse.StatusCode = httpContext.Response.StatusCode;

            // 5. return standard response => return object of ErrorDetails as json


            await httpContext.Response.WriteAsync(resonse.ToString());

        }

        

        private async Task HandleNotFountPointAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";

            var Response = new ErrorDetails()
            {
                StatusCode = httpContext.Response.StatusCode,
                ErrorMessage = $"Error Not Fount this Path {httpContext.Request.Path}"

            }.ToString();

            await httpContext.Response.WriteAsync(Response);

        }

        private int HandileValidationErrorsExpception(ValidationErrorsExpception errorsExpception, ErrorDetails resonse)
        {
            resonse.Errors = errorsExpception.Errors;
            return (int)HttpStatusCode.BadRequest;
        }

    }
}
