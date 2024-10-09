using Domain.Contracts___Interface__;
using Domain.Entities;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specification
{
    public class ProductCountSpecification : Specification<Product>
    {
        public ProductCountSpecification(ProductSpecificationParameter Prams)
        : base(Product =>
            (!Prams.brandid.HasValue || Product.BrandId == Prams.brandid.Value) &&
            (!Prams.typeid.HasValue || Product.TypeId == Prams.typeid.Value)
            )
        {
            
        }
    }
}
