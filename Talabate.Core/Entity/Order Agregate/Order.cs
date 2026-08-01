using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Talabate.Core.Entity.Order_Agregate
{
    public class Order :    BaseEntity
    {

        public Order() { }

        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            this.deliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }= DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public virtual Address ShippingAddress { get; set; }
        public int? DeliveryMethodId { get; set; }

        [ForeignKey("DeliveryMethodId")]
        public virtual DeliveryMethod deliveryMethod { get; set; }
        ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        
        public decimal SubTotal { get; set; }
        public decimal GetTotal()
        {
            return SubTotal + deliveryMethod.Cost;
        }
        public string PaymentIntentId { get; set; } = "";

    }
}
