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
                .Include(nameof(Product.ProductLikes))
                .Include(nameof(Product.ProductImages))
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _dbContext.Products.AsNoTracking()
                .Include(nameof(Product.ProductLikes))
                .Include(nameof(Product.ProductImages))
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategotyAsync(int categoryId)
        {
            return await _dbContext.Products.AsNoTracking()
                .Where(x => x.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByModelAsync(int modelId)
        {
            return await _dbContext.Products.AsNoTracking()
                .Where(x => x.ModelId == modelId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetDeactiveProductsAsync()
        {
            return await _dbContext.Products.AsNoTracking()
                .Where(x => x.IsActive == false)
                .Include(nameof(Product.ProductLikes))
                .Include(nameof(Product.ProductImages))
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetActiveProductsAsync()
        {
            return await _dbContext.Products.AsNoTracking()
                .Where(x => x.IsActive == true)
                .Include (nameof(Product.ProductLikes))
                .Include (nameof(Product.ProductImages))
                .ToListAsync ();
        }



        public async Task<ProductLike> IsLikedByUser(ProductLike input)
        {
            return await _dbContext.ProductLikes.FirstOrDefaultAsync(x => x.UserId == input.UserId && x.ProductId == input.ProductId);
        }

        public async Task<ProductComment> GetCommentAsync(int commentId)
        {
            return await _dbContext.ProductComments.FirstOrDefaultAsync(x => x.Id == commentId);
        }



        public async Task<IEnumerable<ProductComment>> GetProductFiveNewestCommentsAsync(int productId)
        {
            return await _dbContext.ProductComments.AsNoTracking()
                .Where(x => x.ProductId == productId && x.IsApproved == true)
                .OrderByDescending(x => x.DateAdded)
                .Take(5)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductComment>> GetProductTenNewestCommentsAsync(int productId)
        {
            return await _dbContext.ProductComments.AsNoTracking()
                .Where(x => x.ProductId == productId && x.IsApproved == true)
                .OrderByDescending(x => x.DateAdded)
                .Take(10)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductComment>> GetProductAllVerifiedCommentsAsync(int productId)
        {
            return await _dbContext.ProductComments.AsNoTracking()
                .Where(x => x.ProductId == productId && x.IsApproved == true)
                .OrderByDescending(x => x.DateAdded)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductComment>> GetProductUnverifiedCommentsAsync(int productId)
        {
            return await _dbContext.ProductComments.AsNoTracking()
                .Where(x => x.ProductId == productId &&  x.IsApproved == false)
                .OrderBy(x => x.DateAdded)
                .ToListAsync() ;
        }

        public Task<IEnumerable<ProductComment>> GetProductAllCommentsAsync(int productId)
        {
            throw new NotImplementedException();
        }


        public Task<IEnumerable<ProductComment>> GetAllUnverifiedCommentsAsync()
        {
            throw new NotImplementedException();
        }

        

        public Task<IEnumerable<ProductComment>> GetUserAllCommentsAsync(int userId)
        {
            throw new NotImplementedException();
        }



        public Task<IEnumerable<ProductComment>> GetUserUnverifiedCommentsAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductCommentAsync(ProductComment comment)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductCommentAsync(ProductComment comment)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAllProductCommentsAsync(IEnumerable<ProductComment> comments)
        {
            throw new NotImplementedException();
        }
    }
}
