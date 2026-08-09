namespace Talabate.Core.Entity.Identity
{
    public class Address :BaseEntity
    {
        public string FirestName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;

        public string County { get; set; } = null!;
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}