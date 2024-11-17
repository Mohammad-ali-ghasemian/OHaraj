using OHaraj.Core.Domain.Entities.Handling;

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
        public FileManagement? FileManagement { get; set; }
        public int FileManagementId { get; set; }

        public Category? Category { get; set; }
        public int CategoryId { get; set; }

        public Model? Model { get; set; }
        public int ModelId { get; set; }

        public List<CartItem> CartItems { get; set; }

        public List<ProductImages> ProductImages { get; set; }

        public List<ProductLike> ProductLikes { get; set; }

        public List<ProductComment> ProductComments { get; set; }
    }
}
