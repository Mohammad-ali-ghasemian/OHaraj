using Microsoft.EntityFrameworkCore;
using OHaraj.Core.Domain.DTOs;
using OHaraj.Core.Domain.Entities.Handling;
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
                .Include(nameof(Product.ProductLikes))
                .Where(x => x.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByModelAsync(int modelId)
        {
            return await _dbContext.Products.AsNoTracking()
                .Include(nameof(Product.ProductLikes))
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

        public async Task<int> AddFileToTableAsync(FileManagement input)
        {
            await _dbContext.AddAsync(input);
            await _dbContext.SaveChangesAsync();
            return input.Id;
        }

        public async Task<int> UpdateFileToTableAsync(FileManagement input)
        {
            _dbContext.Update(input);
            await _dbContext.SaveChangesAsync();
            return input.Id;
        }

        public async Task<FileManagement> GetFileToTableAsync(int? fileId)
        {
            return await _dbContext.FileManagement.FirstOrDefaultAsync(x => x.Id == fileId);
        }

        public async Task<int> DeleteFileToTableAsync(FileManagement input)
        {
            _dbContext.Remove(input);
            await _dbContext.SaveChangesAsync();
            return input.Id;
        }


        public async Task<ProductLike> IsLikedByUser(ProductLike input)
        {
            return await _dbContext.ProductLikes.FirstOrDefaultAsync(x => x.UserId == input.UserId && x.ProductId == input.ProductId);
        }

        public async Task<ProductComment> GetCommentAsync(int commentId)
        {
            return await _dbContext.ProductComments.FirstOrDefaultAsync(x => x.Id == commentId);
        }

        public async Task AddProductCommentAsync(ProductComment comment)
        {
            await _dbContext.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProductCommentAsync(ProductComment comment)
        {
            _dbContext.Update(comment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductCommentAsync(ProductComment comment)
        {
            _dbContext.Remove(comment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAllProductCommentsAsync(int productId)
        {
            _dbContext.ProductComments.RemoveRange(await GetProductAllCommentsAsync(productId));
            await _dbContext.SaveChangesAsync();
        }



        public async Task<IEnumerable<ProductComment>> GetProductFiveNewestVerifiedCommentsAsync(int productId)
        {
            return await _dbContext.ProductComments.AsNoTracking()
                .Where(x => x.ProductId == productId && x.IsApproved == true)
                .OrderByDescending(x => x.DateAdded)
                .Take(5)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductComment>> GetProductTenNewestVerifiedCommentsAsync(int productId)
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

        public async Task<IEnumerable<ProductComment>> GetProductAllCommentsAsync(int productId)
        {
            return await _dbContext.ProductComments.AsNoTracking()
                .Where(x => x.ProductId == productId)
                .OrderBy(x => x.DateAdded)
                .ToListAsync();
        }



        public async Task<IEnumerable<ProductComment>> GetAllUnverifiedCommentsAsync()
        {
            return await _dbContext.ProductComments.AsNoTracking()
                .Where(x => x.IsApproved == false)
                .OrderBy (x => x.DateAdded)
                .ToListAsync();
        }


        public async Task<IEnumerable<ProductComment>> GetUserAllCommentsAsync(string userId)
        {
            return await _dbContext.ProductComments.AsNoTracking()
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.DateAdded)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductComment>> GetUserUnverifiedCommentsAsync(string userId)
        {
            return await _dbContext.ProductComments.AsNoTracking()
                .Where (x => x.UserId == userId && x.IsApproved == false)
                .OrderBy(x => x.DateAdded)
                .ToListAsync();
        }


    }
}
