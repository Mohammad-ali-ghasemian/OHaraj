using OHaraj.Core.Domain.DTOs;
using OHaraj.Core.Domain.Entities.Shop;

namespace OHaraj.Core.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task AddProductAsync(Product input);
        Task UpdateProductAsync(Product input);
        Task DeleteProductAsync(int id);
        Task<ProductDTO> GetProductAsync(int id);
        Task<IEnumerable<ProductDTO>> GetProductsAsync();
        Task<IEnumerable<ProductDTO>> GetProductsByCategotyAsync(int categoryId);
        Task<IEnumerable<ProductDTO>> GetProductsByModelAsync(int modelId);

    }
}
