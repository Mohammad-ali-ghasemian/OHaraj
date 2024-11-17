namespace OHaraj.Core.Domain.Entities.Shop
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Weight { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public int? Length { get; set; }
        public int Quantity { get; set; }
        public string? ShortContent { get; set; }
        public string? Content { get; set; }
        public string Price { get; set; }
        public int? DiscountPercent { get; set; } = 0;
        public bool IsActive { get; set; } = false;

        // Navigation Properties
        FileManagementId
        CategoryId
        ModelId
    }
}
