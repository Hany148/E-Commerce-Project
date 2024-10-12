using AutoMapper;
using Domain.Contracts___Interface__;
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
        private readonly Lazy<IBasketService> _IBasketService;

        public ServiceManger( IUnitOfWork unitOfWork , IBasketRepository basketRepository  , IMapper _map)
        {
            _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork , _map));
            _IBasketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, _map));
        }
        public IProductService Product => _productService.Value;
        public IBasketService Basket => _IBasketService.Value;
    }
}
