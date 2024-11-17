namespace OHaraj.Core.Domain.Entities.Shop
{
    public class Model
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        // Navigation Properties
        Product
        Category
        FileManagement
    }
}
