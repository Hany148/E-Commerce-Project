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
    [ApiController]
    [Route("Api[Controller]")]
    public class BasketController (IServiceManger serviceManger) : ControllerBase
    {

        [HttpGet("{id}")] // Get baseUrl/api/Basket/value

        public async Task<ActionResult<CustomerBasketDTO>> Get(string id)
        {
            var basket = await serviceManger.Basket.GetBasketAsync(id);
            return Ok(basket);
        }

        [HttpPost]  // Post baseUrl/api/Basket/value
        public async Task<ActionResult<CustomerBasketDTO>> Update(CustomerBasketDTO basketDTO)
        {
            var basket = await serviceManger.Basket.UpdateBasketAsync(basketDTO);
            return Ok(basket);
        }

        [HttpDelete("{id}")] // Delete baseUrl/api/Basket/value
        public async Task<ActionResult> Delete(string id)
        {
            await serviceManger.Basket.DeleteBasketAsync(id);
            return NoContent();
        }

    }
}
