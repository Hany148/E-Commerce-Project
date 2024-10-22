using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntites
{
    public class ProdeuctInOrderItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public string PicutreUrl { get; set; }
    }
}
