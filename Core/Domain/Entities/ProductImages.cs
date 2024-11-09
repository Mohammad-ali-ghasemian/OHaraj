namespace OHaraj.Core.Domain.Entities
{
    public class ProductImages
    {
        public int Id { get; set; }
        public int Order {  get; set; }

        // Navigation Properties
        ProductId
        FileManagementId
    }
}
