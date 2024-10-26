using Domain.Contracts___Interface__;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specification
{
    public class OrderSpecification : Specification<OrderEntity>
    {
        public OrderSpecification(Guid id) 
            : base(order=>order.Id == id)
        {
            AddInclude(order => order.DeliveryMethod);
            AddInclude(order => order.OrderItems);
        }

        public OrderSpecification(string email)
           : base(order => order.UserEmail == email)
        {
            AddInclude(order => order.DeliveryMethod);
            AddInclude(order => order.OrderItems);

            SetOrderBy(order => order.OrderDate);
        }
    }
}
