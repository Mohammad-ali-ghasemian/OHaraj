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
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<IEnumerable<Product>> GetProductsByCategotyAsync(int categoryId);
        Task<IEnumerable<Product>> GetProductsByModelAsync(int modelId);





        Task<IEnumerable<Product>> GetDeactiveProductsAsync();
        Task<IEnumerable<Product>> GetActiveProductsAsync();
        Task<ProductLike> IsLikedByUser(ProductLike input);
        Task<ProductComment> GetCommentAsync(int commentId);
        Task<IEnumerable<ProductComment>> GetProductFiveNewestCommentsAsync(int courseId);
        Task<IEnumerable<ProductComment>> GetProductTenNewestCommentsAsync(int courseId);
        Task<IEnumerable<ProductComment>> GetProductAllVerifiedCommentsAsync(int courseId);
        Task<IEnumerable<ProductComment>> GetProductAllCommentsAsync(int courseId);
        Task<IEnumerable<ProductComment>> GetUserAllCommentsAsync(int userId);
        Task<IEnumerable<ProductComment>> GetAllUnverifiedCommentsAsync();
        Task<IEnumerable<ProductComment>> GetProductUnverifiedCommentsAsync(int courseId);
        Task<IEnumerable<ProductComment>> GetUserUnverifiedCommentsAsync(int userId);
        Task UpdateProductCommentAsync(ProductComment comment);
        Task DeleteProductCommentAsync(ProductComment comment);
        Task DeleteAllProductCommentsAsync(IEnumerable<ProductComment> comments);

    }
}
