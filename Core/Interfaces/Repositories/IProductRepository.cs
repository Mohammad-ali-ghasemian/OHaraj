using OHaraj.Core.Domain.DTOs;
using OHaraj.Core.Domain.Entities.Shop;

namespace OHaraj.Core.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<ProductDTO> AddProductAsync(Product input);
        Task<ProductDTO> UpdateProductAsync(Product input);
        Task<int> DeleteProductAsync(int id);
        Task<ProductDTO> GetProductAsync(int id);
        Task<IEnumerable<ProductDTO>> GetProductsAsync();
        Task<IEnumerable<ProductDTO>> GetProductsByCategotyAsync(int categoryId);
        Task<IEnumerable<ProductDTO>> GetProductsByModelAsync(int modelId);

    }
}
