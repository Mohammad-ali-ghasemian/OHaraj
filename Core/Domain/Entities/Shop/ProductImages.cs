using OHaraj.Core.Domain.Entities.Management;

namespace OHaraj.Core.Domain.Entities.Shop
{
    public class ProductImages
    {
        public int Id { get; set; }
        public int Order { get; set; }

        // Navigation Properties
        public Product Product { get; set; }
        public int ProductId { get; set; }

        public FileManagement FileManagement { get; set; }
        public int FileManagementId { get; set; }
    }
}
