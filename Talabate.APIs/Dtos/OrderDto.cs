using System.ComponentModel.DataAnnotations;
using Talabate.Core.Entity.Order_Agregate;

namespace Talabate.APIs.Dtos
{
    public class OrderDto
    {
        [Required]
        public string BuyerEmail { get; set; }
        [Required]
        public string BasketId { get; set; }
        [Required]
        public int DeliveryMethodId { get; set; }

        public AddressDto ShippingAddress { get; set; }
    }
}
