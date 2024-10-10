using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductSpecificationParameter
    {
        public ProductSorting? sort { get; set; }

        public int? brandid { get; set; }

        public int? typeid { get; set; }

        public int pageIndex { get; set; } = 1;


        private const int MaxPage = 5;
        private const int DefaltPageSize = 5 ;
        private int _pagesize  = DefaltPageSize;
        public int pageSize 
        { 
            
            get
            {
                return _pagesize ;
            }
            
            
            set
            {
              _pagesize = value > MaxPage ? MaxPage :  value;
            }
        
        }

        public string? Search {  get; set; }    
    }

    public enum ProductSorting
    {
        AscendingName = 1,

        DescendingName = 2,

        AscendingPrice = 3,

        DescendingPrice = 4,
    }

}
