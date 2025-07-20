using OHaraj.Core.Domain.DTOs;
using OHaraj.Core.Domain.Models.Product;

namespace OHaraj.Core.Interfaces.Services
{
    public interface IProductService
    {
        Task<ProductDTO> AddProduct(UpsertProduct input);
        Task<ProductDTO> UpdateProduct(UpdateProduct input);
        Task<ProductDTO> GetProduct(int productId);
        Task<IEnumerable<ProductDTO>> GetAllProducts(string filter = null);
        Task<IEnumerable<ProductDTO>> GetProductsByCategory(int categoryId);
        Task<IEnumerable<ProductDTO>> GetProductsByModel(int ModelId);
        Task<int> DeleteProduct(int productId);
    }
}
