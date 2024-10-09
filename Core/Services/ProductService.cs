global using Domain.Contracts.IUnitOfWork;
global using Shared.DTO;
using AutoMapper;
using Domain.Contracts___Interface__;
using Domain.Entities;
using Domain.Exceptions;
using Persistence.Exceptions;
using Services.Specification;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public class ProductService : IProductService
    {

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductBrandDTO>> GetProductsBrandDTOAsync()
        {
            // 1. Retrive all ProductBrand by using IUnitOfWork interface
            var productBrands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync(true);

            // 2. Mapping to ProductBrandDTO By using package of Automapper
            var productBrandsDto = _mapper.Map<IEnumerable<ProductBrandDTO>>(productBrands);

            // 3. Return

            return productBrandsDto;
        }

        public async Task<PageinationResult<ProductDTO>> GetProductsDTOAsync(ProductSpecificationParameter Prams)
        {
            // 1. Retrive all ProductBrand by using IUnitOfWork interface
            var products = await _unitOfWork.GetRepository<Product, int>()
                .GetAllAsync(new ProductWithBrandAndTypeSpecification(Prams));

            // 2. Mapping to ProductBrandDTO By using package of Automapper
            var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);

            // 3. Retrive Count of Product
            var TotalCountOfProduct = await _unitOfWork.GetRepository<Product, int>()
                .CountAsync(new ProductCountSpecification(Prams));

            // 4. Mappint to PageinationResult
            var Rsult = new PageinationResult<ProductDTO>(
                   Prams.pageIndex,
                   Prams.pageSize,
                  TotalCountOfProduct,
                  productsDto

            );
            // 5. Return

            return Rsult;
        }

        public async Task<ProductDTO> GetProductsDTOByIdAsync(int id)
        {
            // 1. Retrive all ProductBrand by using IUnitOfWork interface
            var product = await _unitOfWork.GetRepository<Product, int>()
                .FindByIdAysnc(new ProductWithBrandAndTypeSpecification(id));

            // 2. if product is null 
            if(product is null)
            {
                throw new ProductNotFoundExceptions(id);
            }
            // 3. Mapping to ProductBrandDTO By using package of Automapper
            var productDto = _mapper.Map<ProductDTO>(product);

            // 4. Return

            return productDto;
        }

        public async Task<IEnumerable<ProductTypeDTO>> GetProductsTypeDTOAsync()
        {
            // 1. Retrive all ProductBrand by using IUnitOfWork interface
            var productTypes = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync(true);

            // 2. Mapping to ProductBrandDTO By using package of Automapper
            var productTypesDto = _mapper.Map<IEnumerable<ProductTypeDTO>>(productTypes);

            // 3. Return

            return productTypesDto;
        }
    }
}
