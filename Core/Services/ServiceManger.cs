using AutoMapper;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManger : IServiceManger
    {

        private readonly Lazy<IProductService> _productService;

        public ServiceManger( IUnitOfWork unitOfWork  , IMapper _map)
        {
            _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork , _map));
        }
        public IProductService Product => _productService.Value;
    }
}
