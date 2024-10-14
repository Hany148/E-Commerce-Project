using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts___Interface__
{
    public interface IBasketRepository
    {
        public Task<CustomerBasket?> GetBasketAsync(string id);

        public Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket , TimeSpan? TTL = null);

        /*
                      Time To live ( وقت اللي يعيش فيه )  
           , في الرام دي خليها تعيش في الرام قد ايه وبعدين امسحها create اللي هعملها basket يعني ال

        */


        public Task<bool> DeleteBasketAsync(string id);

    }
}
