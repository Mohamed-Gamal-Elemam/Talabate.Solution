using System.ComponentModel.DataAnnotations;

namespace Talabate.APIs.Dtos
{
    public class AddressDto
    {
        [Required]
        public string FirestName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string County { get; set; }
    }
}