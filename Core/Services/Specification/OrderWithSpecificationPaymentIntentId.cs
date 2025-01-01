using Domain.Contracts___Interface__;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specification
{
    public class OrderWithSpecificationPaymentIntentId : Specification<OrderEntity>
    {
        public OrderWithSpecificationPaymentIntentId(string PaymentIntentId) :
            base(Order=>Order.PaymentIntentId == PaymentIntentId)
        {
        }
    }
}
