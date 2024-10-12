using Domain.Contracts___Interface__;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connectionMultiplexer) : IBasketRepository
    {

        private readonly IDatabase _database = connectionMultiplexer.GetDatabase();
        public async Task<bool> DeleteBasketAsync(string id)
        {
           return await _database.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
            var BasketJson = await _database.StringGetAsync(id);
            if (BasketJson.IsNullOrEmpty)
            {
                return null;
            }
            return JsonSerializer.Deserialize<CustomerBasket?>(BasketJson!);
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket, TimeSpan? TTL = null)
        {
            var BasketToJson = JsonSerializer.Serialize(basket);

            var IsCreatedOrUpdated = await _database.StringSetAsync(basket.Id, BasketToJson, TTL ?? TimeSpan.FromDays(15));

            return IsCreatedOrUpdated ? await GetBasketAsync(basket.Id) : null  ;

        }
    }
}
