using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductSpecificationParameter
    {
      public ProductSorting? sort; 
     
      public  int? brandid; 
    
      public  int? typeid;
    }

    public enum ProductSorting
    {
        AscendingName = 1,
        
        DescendingName = 2,

        AscendingPrice = 3,

        DescendingPrice = 4,
    }

}
