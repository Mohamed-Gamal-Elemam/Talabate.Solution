using System.ComponentModel.DataAnnotations;
using Talabate.Core.Entity.Basket;

namespace Talabate.APIs.Dtos
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; } = null!;
        [Required]
        public List<BasketItemDto> Items { get; set; }
    }
}
