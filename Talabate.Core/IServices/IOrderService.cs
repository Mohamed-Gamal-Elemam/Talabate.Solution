using System;
using System.Collections.Generic;
using System.Text;
using Talabate.Core.Entity.Order_Agregate;

namespace Talabate.Core.IServices
{
    public interface IOrderService
    {
        Task<Order?> CreateOrderAsync (string BuyerEmail, string BasketId, int DeliverMethodId, Address ShippingAddress);
        Task<IReadOnlyList<Order?>> GetOrdersForUserAsync(string BuyerEmail);
        Task<Order?> GetOrderByIdForUserAsync(int OrderId,string BuyerEmail);

        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethods();
    }
}
