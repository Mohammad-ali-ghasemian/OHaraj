using Microsoft.AspNetCore.Identity;

namespace OHaraj.Core.Domain.Entities.Shop
{
    public class ProductComment
    {
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsApproved { get; set; }

        // Navigation Properties
        public IdentityUser IdentityUser { get; set; }
        public string UserId { get; set; }

        public Product Product { get; set; }
        public int ProductId { get; set; }
    }
}
