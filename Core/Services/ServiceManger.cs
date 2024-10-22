using AutoMapper;
using Domain.Contracts___Interface__;
using Domain.Idntity_Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Services.Abstractions;
using Shared;
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
        private readonly Lazy<IAuthenticationServices> _authenticationServices;
        private readonly Lazy<IOrderService> _Order;

        public ServiceManger( IUnitOfWork unitOfWork ,
            IBasketRepository basketRepository  , 
            IMapper _map
            , UserManager<User> userManger 
            , IOptions<JWTOptions> options )
        {
            _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork , _map));
            _IBasketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, _map));
            _authenticationServices = new Lazy<IAuthenticationServices>(()=> new AuthenticationServices(userManger , options));
            _Order = new Lazy<IOrderService>(()=> new OrderService(_map , unitOfWork , basketRepository));
        }
        public IProductService Product => _productService.Value;
        public IBasketService Basket => _IBasketService.Value;
        public  IAuthenticationServices authentication => _authenticationServices.Value;
        public  IOrderService Order => _Order.Value;
    }
}
