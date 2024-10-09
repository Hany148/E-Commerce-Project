using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  Shared;

namespace Domain.Contracts___Interface__
{
    public class ProductWithBrandAndTypeSpecification : Specification<Product>
    {
        // use to retrieve product by id
        public ProductWithBrandAndTypeSpecification(int id)
            : base(product => product.Id == id)
        {
            AddInclude(product => product.productBrand);
            AddInclude(product => product.ProductType);
        }

        // use to retrieve all product 
        public ProductWithBrandAndTypeSpecification
            (ProductSpecificationParameter Prams)
            : base(Product=>
            (!Prams.brandid.HasValue || Product.BrandId== Prams.brandid.Value) &&
            (!Prams.typeid.HasValue || Product.TypeId == Prams.typeid.Value) 
            )
        {
            AddInclude(product => product.productBrand);
            AddInclude(product => product.ProductType);

            if (Prams.sort is not null)
            {
                switch (Prams.sort)
                {
                    case ProductSorting.DescendingName:  {
                            SetOrderByDec(p => p.Price);
                            break;
                        }

                    case ProductSorting.AscendingName:
                        {
                            SetOrderBy(p => p.Price);
                            break;
                        }

                    case ProductSorting.DescendingPrice:
                        {
                            SetOrderByDec(p => p.Name);
                            break;
                        }

                    default :
                        {
                            SetOrderBy(p => p.Name);
                            break;
                        }

                }
            }

        }


    }
}
