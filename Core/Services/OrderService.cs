using AutoMapper;
using Domain.Contracts___Interface__;
using Domain.Entities;
using Domain.Entities.OrderEntites;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Persistence.Exceptions;
using Services.Abstractions;
using Services.Specification;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService (IMapper mapper , IUnitOfWork unitOfWork , IBasketRepository basketRepository)
        : IOrderService
    {

        public async Task<OrderDTO> CreateOrderAsync(ParameterOfOrder parameterOfOrder, string UserEmail)
        {
            // 1. Address
            var ShippingAdress = mapper.Map<Address>(parameterOfOrder.ShippingAdress);

            // 2. order items => basket => basket items => 

            var basket = await basketRepository.GetBasketAsync(parameterOfOrder.BasketId)
                ?? throw new BasketNotFoundException(parameterOfOrder.BasketId);

            var orderItems = new List<OrderItem>();

            foreach (var item in basket.BasketItems)
            {
                var product = await unitOfWork.GetRepository<Product, int>().FindByIdAysnc(item.Id)
                    ?? throw new ProductNotFoundExceptions(item.Id);

                orderItems.Add(CreateOrderItem(item , product));
            }

            // 3. Delivary Method
            var delivaryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>()
                .FindByIdAysnc(parameterOfOrder.DeliveryMethodId) 
                ?? throw new DeliveryMethodNotFoundExceptions(parameterOfOrder.DeliveryMethodId);

            // 4. subTotal
            var subTotal = orderItems.Sum(o => (o.Price * o.Quntity));

            // 5. create order 

            var order = new Order(UserEmail , ShippingAdress , orderItems , delivaryMethod , subTotal);

            // 5. save to data base 
            await unitOfWork.GetRepository<Order, Guid>()
                .AddAsync(order);
                
            await unitOfWork.ToSaveChanges();

            // 6. map to orderDto

            return mapper.Map<OrderDTO>(order);

        }

        private OrderItem CreateOrderItem(BasketItem item, Product product)
        {
            return new OrderItem(new ProdeuctInOrderItem(product.Id , product.Name , product.PictureUrl) , item.Quntity , product.Price) ;
        }


        public async Task<IEnumerable<DeliveryMethodDTO>> GetAllDeliveryMethodAsync()
        {
            var delivaryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return mapper.Map<IEnumerable<DeliveryMethodDTO>>(delivaryMethod);
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersOfUserAsync(string Email)
        {

            var delivaryMethod = await unitOfWork.GetRepository<Order, Guid>().GetAllAsync(new OrderSpecification(Email))
                ?? throw new OrderNotFoundException(Email);
            return mapper.Map<IEnumerable<OrderDTO>>(delivaryMethod);
        }

        public async Task<OrderDTO> GetOrderAsync(Guid id)
        {
            var Order = await unitOfWork.GetRepository<Order, Guid>().FindByIdAysnc(new OrderSpecification(id))
                ?? throw new OrderNotFoundException(id);
            return mapper.Map<OrderDTO>(Order);
        }
    }
}
