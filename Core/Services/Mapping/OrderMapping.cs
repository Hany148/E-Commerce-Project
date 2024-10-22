global using OrderEntity = Domain.Entities.OrderEntites.Order;
using AutoMapper;
using Domain.Entities.OrderEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mapping
{
    public class OrderMapping : Profile
    {
        public OrderMapping() 
        {
            CreateMap<OrderEntity, OrderDTO>();
        }
    }

}
