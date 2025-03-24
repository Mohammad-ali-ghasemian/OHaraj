using OHaraj.Core.Domain.DTOs;
using OHaraj.Core.Domain.Models.Product;

namespace OHaraj.Core.Interfaces.Services
{
    public interface IProductService
    {
        Task<ProductDTO> AddProduct(Product product);
        Task<ProductDTO> UpdateProduct(Product product);
        Task<ProductDTO> GetProduct(int productId);
        Task<IEnumerable<ProductDTO>> GetAllProducts();
        Task<IEnumerable<ProductDTO>> GetProductsByCategory(int categoryId);
        Task<int> DeleteProduct(int productId);
    }
}
