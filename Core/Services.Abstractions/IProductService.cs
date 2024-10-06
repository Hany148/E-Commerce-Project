using Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IProductService
    {
        // 1. Retrive Get all Product 

        public Task<IEnumerable<ProductDTO>> GetProductsDTOAsync(string? sort, int? brandid, int? typeid);

        // 2. Retrive Get all Product Type
        public Task<IEnumerable<ProductTypeDTO>> GetProductsTypeDTOAsync();

        // 3. Retrive Get all Product Brand
        public Task<IEnumerable<ProductBrandDTO>> GetProductsBrandDTOAsync();

        // 4. Retrive Get Product by Id
        public Task<ProductDTO> GetProductsDTOByIdAsync(int id);

    }
}
