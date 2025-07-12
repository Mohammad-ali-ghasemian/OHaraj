using OHaraj.Core.Domain.DTOs;
using OHaraj.Core.Domain.Entities.Shop;

namespace OHaraj.Core.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task AddProductAsync(Product input);
        Task UpdateProductAsync(Product input);
        Task DeleteProductAsync(Product input);
        Task<Product> GetProductAsync(int id);
        Task<Product> GetProductDetailsAsync(int id);
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<IEnumerable<Product>> GetProductsByCategotyAsync(int categoryId);
        Task<IEnumerable<Product>> GetProductsByModelAsync(int modelId);

    }
}
