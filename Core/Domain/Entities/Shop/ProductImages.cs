namespace OHaraj.Core.Domain.Entities.Shop
{
    public class ProductImages
    {
        public int Id { get; set; }
        public int Order { get; set; }

        // Navigation Properties
        ProductId
        FileManagementId
    }
}
