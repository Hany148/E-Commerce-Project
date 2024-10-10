using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Exceptions
{
    public sealed class ProductNotFoundExceptions : NotFoundException
    {
        public ProductNotFoundExceptions(int id)
            : base($"Product with id : {id} not found")
        {
            
        }
    }
}
