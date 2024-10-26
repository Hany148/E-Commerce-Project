using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntites
{
    public class ProdeuctInOrderItem
    {
        public ProdeuctInOrderItem() { }
        public ProdeuctInOrderItem(int productId, string productName, string picutreUrl)
        {
            ProductId = productId;
            ProductName = productName;
            PicutreUrl = picutreUrl;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public string PicutreUrl { get; set; }
    }
}
