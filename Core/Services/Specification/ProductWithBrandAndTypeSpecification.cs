using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            (string? sort, int? brandid, int? typeid)
            : base(Product=>
            (!brandid.HasValue || Product.BrandId== brandid.Value) &&
            (!typeid.HasValue || Product.TypeId == typeid.Value) 
            )
        {
            AddInclude(product => product.productBrand);
            AddInclude(product => product.ProductType);

            if (!string.IsNullOrWhiteSpace(sort))
            {
                switch (sort.ToLower().Trim())
                {
                    case "pricedesc":  {
                            SetOrderByDec(p => p.Price);
                            break;
                        }

                    case "priceasc":
                        {
                            SetOrderBy(p => p.Price);
                            break;
                        }

                    case "namedesc":
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
