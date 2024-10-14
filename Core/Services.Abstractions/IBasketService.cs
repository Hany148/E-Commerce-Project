using Shared.BsketDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IBasketService
    {
        public  Task<bool> DeleteBasketAsync(string id);


        public  Task<CustomerBasketDTO?> GetBasketAsync(string id);


        public  Task<CustomerBasketDTO?> UpdateBasketAsync(CustomerBasketDTO basket, TimeSpan? TTL = null);
      
    }
}
