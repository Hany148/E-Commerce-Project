global using ProductService = Services.Abstractions.ProductService;
using AutoMapper;
using Domain.Contracts___Interface__;
using Domain.Idntity_Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
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
        private readonly Lazy<IPaymentService> _paymentService;

        public ServiceManger( IUnitOfWork unitOfWork ,
            IBasketRepository basketRepository  , 
            IMapper map
            , UserManager<User> userManger 
            , IOptions<JWTOptions> options,
            IConfiguration configuration)

        {
            _productService = new Lazy<IProductService>(() => new ProductService(unitOfWork , map));
            _IBasketService = new Lazy<IBasketService>(() => new BasketService(basketRepository, map));
            _authenticationServices = new Lazy<IAuthenticationServices>(()=> new AuthenticationServices(userManger , options));
            _Order = new Lazy<IOrderService>(()=> new OrderService(map, unitOfWork , basketRepository));
            _paymentService = new Lazy<IPaymentService>(()=> new PaymentService(basketRepository , unitOfWork , map, configuration));
        }
        public IProductService Product => _productService.Value;
        public IBasketService Basket => _IBasketService.Value;
        public  IAuthenticationServices authentication => _authenticationServices.Value;
        public  IOrderService Order => _Order.Value;
        public IPaymentService PaymentService => _paymentService.Value;

    }
}
