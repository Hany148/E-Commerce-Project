using AutoMapper;
using Domain.Entities;
using Shared.BsketDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mapping
{
    public class MappingBasket : Profile
    {
        public MappingBasket()
        {

            CreateMap<CustomerBasket, CustomerBasketDTO>().ReverseMap();
            CreateMap<BasketItem, BasketItemDTO>().ReverseMap();
        }
    }
}
