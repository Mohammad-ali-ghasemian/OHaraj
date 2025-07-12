using OHaraj.Core.Domain.DTOs;
using OHaraj.Core.Domain.Entities.Shop;
using OHaraj.Core.Interfaces.Repositories;
using OHaraj.Infrastructure;

namespace OHaraj.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddProductAsync(Product input)
        {
            await _dbContext.AddAsync(input);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product input)
        {
            _dbContext.Update(input);
            await _dbContext.SaveChangesAsync();
        }

        public Task DeleteProductAsync(Product input)
        {
            _dbContext.Remove
        }

        public Task<ProductDTO> GetProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDTO>> GetProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDTO>> GetProductsByCategotyAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDTO>> GetProductsByModelAsync(int modelId)
        {
            throw new NotImplementedException();
        }
    }
}
