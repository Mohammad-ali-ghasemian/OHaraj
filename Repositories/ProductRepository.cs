using Microsoft.EntityFrameworkCore;
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

        public async Task DeleteProductAsync(Product input)
        {
            _dbContext.Remove(input);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Product> GetProductAsync(int id)
        
        }

        public Task<IEnumerable<Product>> GetProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductsByCategotyAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductsByModelAsync(int modelId)
        {
            throw new NotImplementedException();
        }
    }
}
