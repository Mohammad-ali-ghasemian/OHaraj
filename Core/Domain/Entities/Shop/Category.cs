namespace OHaraj.Core.Domain.Entities.Shop
{
    public class Category
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        // Navigation Properties
        Product
        Model
        FileManagementId
    }
}
