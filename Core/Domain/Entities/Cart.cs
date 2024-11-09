using OHaraj.Core.Enums;

namespace OHaraj.Core.Domain.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DatePaid { get; set; }
        public int TotalAmount { get; set; }
        public CartStatus Status { get; set; }

        // Navigation Properties
        UserId
    }
}
