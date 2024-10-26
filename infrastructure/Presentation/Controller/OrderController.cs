using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{

    [Authorize]
    public class OrderController (IServiceManger serviceManger) : APIController
    {

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> Create (ParameterOfOrder parameterOfOrder)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orderDTO = await serviceManger.Order.CreateOrderAsync(parameterOfOrder , email!);

            return Ok(orderDTO);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAllOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orderDTO = await serviceManger.Order.GetAllOrdersOfUserAsync(email!);

            return Ok(orderDTO);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetAllOrder(Guid id)
        {
            
            var orderDTO = await serviceManger.Order.GetOrderAsync(id!);

            return Ok(orderDTO);
        }

        [HttpGet("deliveryMethod")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDTO>>> GetAllProduc()
        {

            var DeliveryMethods = await serviceManger.Order.GetAllDeliveryMethodAsync();

            return Ok(DeliveryMethods);
        }

    }
}
