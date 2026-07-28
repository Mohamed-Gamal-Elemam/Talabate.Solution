using System;
using System.Collections.Generic;
using System.Text;
using Talabate.Core.Entity.Basket;

namespace Talabate.Core.IRepos
{
    public interface IBasketRepo
    {
        Task<CustomerBasket?> GetBasketAsync(string basketId);
        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
