global using Shared.DTO;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IOrderService
    {

        // retrieve get order by id 

        public Task<OrderDTO> GetOrderAsync();

        // get orders for user by email 

        public Task<IEnumerable<OrderDTO>> GetAllOrderAsync(string Email);

        // create order 

        public Task<OrderDTO> CreateOrderAsync(ParameterOfOrder orderDTO , string UserEmail);

        // get all Delivery Method

        public Task<DeliveryMethodDTO> GetAllDeliveryMethodAsync();

    }
}
