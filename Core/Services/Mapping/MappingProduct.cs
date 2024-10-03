﻿using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Mapping
{
    public class MappingProduct : Profile
    {
        public MappingProduct()
        {
            CreateMap<Product , ProductDTO >()
                .ForMember(pd=>pd.BrandName , option=>option.MapFrom(p=>p.productBrand.Name))
                .ForMember(pd=>pd.BrandName , option=>option.MapFrom(p=>p.ProductType.Name))
                .ForMember(pd=>pd.PictureUrl , option=>option.MapFrom<PictureURLResolver>())
                .ReverseMap();

            CreateMap<ProductBrand, ProductBrandDTO>()
                .ReverseMap();

            CreateMap<ProductTypeDTO, ProductTypeDTO>()
                .ReverseMap();
        }
    }
}
