using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.BsketDTO;
using System;
using System.Collections.Generic;
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
    }
}
