using System;
using System.Collections.Generic;
using System.Text;
using Talabate.Core.Entity.Order_Agregate;
using Talabate.Core.Entity.Product;
using Talabate.Core.IRepos;
using Talabate.Core.IServices;
using Talabate.Core.IUnitOfWork;

namespace Talabate.Application.Services
{
    public class OrderService : IOrderService
    {
       
        private readonly IUnitOfWork _unitOfWork;

        public IBasketRepo _basketRepo { get; }
   

        public OrderService(IBasketRepo basketRepo,IUnitOfWork unitOfWork)
        {
            _basketRepo = basketRepo;
            _unitOfWork = unitOfWork;
        }



        public async Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliverMethodId, Address ShippingAddress)
        {
            // get basket

            var basket =await _basketRepo.GetBasketAsync(BasketId);
            var OrderItems=new List<OrderItem>();

            //get orderitem
            if (basket?.Items?.Count()>0)
            {
                foreach (var item in basket.Items)
                {
                    var Product = await _unitOfWork.Reposatory<Product>().GetAsync(item.Id);
                    var ProductItemOrder= new ProductItemOrder(item.Id,Product.Name,item.PictureUrl);
                    var orderItem = new OrderItem(ProductItemOrder, item.Quantity, Product.Price);
                    OrderItems.Add(orderItem);

                    
                }

            }

            //subtotal

            var SubTotal = OrderItems.Sum(orderitem=> orderitem.Price *orderitem.Quantity);

            //delivery Method

            var DeliveryMethod = await _unitOfWork.Reposatory<DeliveryMethod>().GetAsync(DeliverMethodId);


            //order
            var Order =new Order(BuyerEmail, ShippingAddress,DeliveryMethod,OrderItems,SubTotal);

             await _unitOfWork.Reposatory<Order>().AddAsync(Order);

            //save in data base
            var result =await _unitOfWork.CompleteAsync();

            if (result <= 0) return null;

            return (Order);

        }

        public Task<Order> GetOrderByIdForUserAsync(int OrderId, string BuyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string BuyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
