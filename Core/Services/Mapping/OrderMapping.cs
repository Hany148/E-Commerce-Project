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

            CreateMap<OrderEntity, OrderDTO>()
                .ForMember(od => od.paymentStatus, option => option.MapFrom(o => o.paymentStatus.ToString()))
                .ForMember(od => od.DeliveryMethod, option => option.MapFrom(o => o.DeliveryMethod.ShortName))
                .ForMember(od => od.Total, option => option.MapFrom(o => ( o.SubTotal + o.DeliveryMethod.Price)))

                .ReverseMap()
                .ForMember(od => od.paymentStatus, option => option.MapFrom(o => Enum.Parse<OrderPaymentStatus>(o.paymentStatus)))
;


            CreateMap<Address, AddressDTO>().ReverseMap();

            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(OID => OID.ProductId , option=> option.MapFrom(OI => OI.Product.ProductId))
                .ForMember(OID => OID.ProductName , option=> option.MapFrom(OI => OI.Product.ProductName))
                .ForMember(OID => OID.PicutreUrl , option=> option.MapFrom(OI => OI.Product.PicutreUrl))
                .ReverseMap();


            CreateMap<DeliveryMethod, DeliveryMethodDTO>().ReverseMap();

        }
    }

}
