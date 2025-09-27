namespace OHaraj.Core.Domain.DTOs
{
    public class ProductDTO
    {
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
        public int CategoryId { get; set; }
        public int ModelId { get; set; }
        public int LikesNumber { get; set; }

        public string? MainImagePath { get; set; }
        public List<string>? OtherImagesPath { get; set; }
    }
}
