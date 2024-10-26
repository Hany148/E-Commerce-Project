using Domain.Entities.OrderEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class OrderNotFoundException : NotFoundException
    {
        public OrderNotFoundException(string Email)
            : base($"Orders Of Email {Email} is not found")
        {
        }

        public OrderNotFoundException(Guid id)
           : base($"Order Of id {id} is not found")
        {
        }
    }
}
