using StackExchange.Redis;
using System.Text.Json;
using Talabate.Core.Entity.Basket;
using Talabate.Core.IRepos;

namespace Talabate.Infrastructer.BasketRepo
{
    public class BasketRepo : IBasketRepo
    {
        private readonly IDatabase _database;
        public BasketRepo(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var basket = await _database.StringGetAsync(basketId);
            return basket.IsNullOrEmpty ? null :JsonSerializer.Deserialize<CustomerBasket>((string)basket!);


        }



        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            var CreatedOrUpdated = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
            if (! CreatedOrUpdated ) return null;

            return await GetBasketAsync(basket.Id);
              
        }



        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }


    }
}
