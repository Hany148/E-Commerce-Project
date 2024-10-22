using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.OrderEntites
{
    public class OrderItem : BaseEntity<Guid>
    {

        public ProdeuctInOrderItem Product { get; set; }
        public int Quntity { get; set; }
        public decimal Price { get; set; }
    }
}
