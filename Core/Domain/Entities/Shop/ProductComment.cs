namespace OHaraj.Core.Domain.Entities.Shop
{
    public class ProductComment
    {
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsApproved { get; set; }

        // Navigation Properties
        ProductId
        UserId
    }
}
