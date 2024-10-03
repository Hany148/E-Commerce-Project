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
        public ProductWithBrandAndTypeSpecification (int id)
            : base(product=>product.Id == id)   
        {
            AddInclude(product => product.productBrand);
            AddInclude(product => product.ProductType);
        }

        // use to retrieve all product 
        public ProductWithBrandAndTypeSpecification()
            : base(null)
        {
            AddInclude(product => product.productBrand);
            AddInclude(product => product.ProductType);
        }


    }
}
