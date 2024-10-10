using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace E_Commerce_API.Response_Factory
{
    public class ApiResponseFactory
    {
        public static IActionResult CustomValidation(ActionContext actionContext)
        {
            // 1. Get All Errors From model state in actionContext
            
                  var errors = actionContext.ModelState.Where(error => error.Value.Errors.Any())
                      .Select(error => new ValidationError()
                      {
                          Feild = error.Key,
                          Errors = error.Value.Errors.Select(e => e.ErrorMessage)
                      });

            // 2. create custom respose 

            var Response = new ErrorValidationResonse()
            {
                statusCode = (int)HttpStatusCode.BadRequest,
                Errormessage = "Validation Errors",
                Errors = errors
            };

            // 3. return BadRequestObjectResult 
            return new BadRequestObjectResult(Response);
        }
    }
}
