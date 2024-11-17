using Microsoft.AspNetCore.Identity;

namespace OHaraj.Core.Domain.Entities.Shop
{
    public class ProductLike
    {
        public int Id { get; set; }

        // Navigation Properties
        public IdentityUser IdentityUser { get; set; }
        public string UserId {  get; set; }

        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
