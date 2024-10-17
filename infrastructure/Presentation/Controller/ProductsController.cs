using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using Shared.DTO;
using Shared.ErrorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controller
{
  
    public class ProductsController : APIController
    {

        private readonly IServiceManger _serviceManger;

        public ProductsController(IServiceManger serviceManger)
        {
            _serviceManger = serviceManger;
        }


        // [Authorize (Roles = "Admin")]
        [HttpGet]
     
        public async Task<ActionResult<PageinationResult<ProductDTO>>> GetAllProduct([FromQuery]ProductSpecificationParameter Prams)
        {
            var products = await _serviceManger.Product.GetProductsDTOAsync(Prams);
            return Ok(products);
        }

        [HttpGet("Brand")]
        public async Task<ActionResult<IEnumerable<ProductBrandDTO>>> GetAllProductBrand()
        {
            var productsBrand = await _serviceManger.Product.GetProductsBrandDTOAsync();
            return Ok(productsBrand);
        }

        [HttpGet("Type")]
        public async Task<ActionResult<IEnumerable<ProductTypeDTO>>> GetAllTypeProduct()
        {
            var productsType = await _serviceManger.Product.GetProductsTypeDTOAsync();
            return Ok(productsType);
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var product = await _serviceManger.Product.GetProductsDTOByIdAsync(id);
            return Ok(product);
        }


    }
}
