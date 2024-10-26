using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO
{
    public record OrderItemDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PicutreUrl { get; set; }
        public int Quntity { get; set; }
        public decimal Price { get; set; }
    }
}
