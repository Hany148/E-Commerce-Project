using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.BsketDTO;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
    public class PaymentController(IServiceManger serviceManger)
        : APIController
    {
        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDTO>> CreateOrUpdatePayment(string basketId)
        {
            var Result = await serviceManger.PaymentService.CreateOrUpdatePaymentIntentAsync(basketId);
            return Result;
        }




        [HttpPost("webHook")]
        public async Task<IActionResult> WebHook()
        {
            
            var json = await new StreamReader(stream: HttpContext.Request.Body).ReadToEndAsync();
            await serviceManger.PaymentService.UpdateOrderPaymentStatusAsync(json , Request.Headers["Stripe-Signature"]!);

            return new EmptyResult();

        }

    }
}
