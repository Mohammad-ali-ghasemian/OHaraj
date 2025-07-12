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
        {
            return await _dbContext.Products.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product> GetProductDetailsAsync(int id)
        {
            return await _dbContext.Products.AsNoTracking()
                .Include(nameof(Product.ProductLikes))
                .Include(nameof(Product.ProductComments))
                .Include(nameof(Product.ProductImages))
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _dbContext.Products.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategotyAsync(int categoryId)
        {
            return await _dbContext.Products.AsNoTracking()
                .Where(x => x.CategoryId == categoryId)
                .ToListAsync();
        }

        public Task<IEnumerable<Product>> GetProductsByModelAsync(int modelId)
        {
            throw new NotImplementedException();
        }
    }
}
