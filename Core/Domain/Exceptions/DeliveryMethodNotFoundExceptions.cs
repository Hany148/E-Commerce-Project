using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class DeliveryMethodNotFoundExceptions : NotFoundException
    {
        public DeliveryMethodNotFoundExceptions(int id) : base($"DeliveryMethodId of id {id} is not found")
        {
        }
    }
}
