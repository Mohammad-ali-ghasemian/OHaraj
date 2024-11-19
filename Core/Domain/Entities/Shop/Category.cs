using OHaraj.Core.Domain.Entities.Handling;

namespace OHaraj.Core.Domain.Entities.Shop
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        // Navigation Properties
        public List<Product> Products { get; set; }

        public List<Model> Models { get; set; }

        public FileManagement FileManagement { get; set; }
        public int FileManagementId { get; set; }

        public Category ParentCategory { get; set; }
        public int ParentId { get; set; }
    }
}
