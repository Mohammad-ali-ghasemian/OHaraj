using Microsoft.AspNetCore.Identity;
using OHaraj.Core.Enums;

namespace OHaraj.Core.Domain.Entities.Shop
{
    public class Cart
    {
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DatePaid { get; set; }
        public int TotalAmount { get; set; }
        public CartStatus Status { get; set; }

        // Navigation Properties
        public IdentityUser User { get; set; }
        public string UserId { get; set; } = string.Empty;

        public List<CartItem> Items { get; set; }
    }
}
