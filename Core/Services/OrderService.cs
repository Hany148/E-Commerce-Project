using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IOrderService
    {
        public Task<OrderDTO> CreateOrderAsync(ParameterOfOrder orderDTO, string UserEmail)
        {
            throw new NotImplementedException();
        }

        public Task<DeliveryMethodDTO> GetAllDeliveryMethodAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderDTO>> GetAllOrderAsync(string Email)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDTO> GetOrderAsync()
        {
            throw new NotImplementedException();
        }
    }
}
