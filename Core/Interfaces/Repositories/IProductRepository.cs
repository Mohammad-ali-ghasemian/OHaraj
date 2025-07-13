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





        Task<IEnumerable<Product>> GetUnverifiedProductsAsync();
        Task<IEnumerable<Product>> GetUnverifiedProductsAsync(int userId);
        Task<ProductLike> IsLiked(ProductLike input);
        Task<ProductComment> GetProductCommentAsync(int commentId);
        Task<IEnumerable<ProductComment>> GetProductFiveCommentsAsync(int courseId);
        Task<IEnumerable<ProductComment>> GetProductTenCommentsAsync(int courseId);
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
