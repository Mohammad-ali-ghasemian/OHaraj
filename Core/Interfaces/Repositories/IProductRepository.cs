using OHaraj.Core.Domain.DTOs;
using OHaraj.Core.Domain.Entities.Handling;
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
        Task<IEnumerable<Product>> GetDeactiveProductsAsync();
        Task<IEnumerable<Product>> GetActiveProductsAsync();
        Task<IEnumerable<Product>> GetProductsByCategotyAsync(int categoryId);
        Task<IEnumerable<Product>> GetProductsByModelAsync(int modelId);

        Task<int> AddFileToTableAsync(FileManagement input);
        Task<int> UpdateFileToTableAsync(FileManagement input);
        Task<FileManagement> GetFileToTableAsync(int? fileId);
        Task<int> DeleteFileToTableAsync(FileManagement input);




        Task<ProductLike> IsLikedByUser(ProductLike input);
        Task<ProductComment> GetCommentAsync(int commentId);
        Task AddProductCommentAsync(ProductComment comment);
        Task UpdateProductCommentAsync(ProductComment comment);
        Task DeleteProductCommentAsync(ProductComment comment);
        Task DeleteAllProductCommentsAsync(int productId);

        
        Task<IEnumerable<ProductComment>> GetProductFiveNewestVerifiedCommentsAsync(int courseId);
        Task<IEnumerable<ProductComment>> GetProductTenNewestVerifiedCommentsAsync(int courseId);
        Task<IEnumerable<ProductComment>> GetProductAllVerifiedCommentsAsync(int courseId);
        Task<IEnumerable<ProductComment>> GetProductUnverifiedCommentsAsync(int courseId);
        Task<IEnumerable<ProductComment>> GetProductAllCommentsAsync(int courseId);


        Task<IEnumerable<ProductComment>> GetAllUnverifiedCommentsAsync();

        Task<IEnumerable<ProductComment>> GetUserAllCommentsAsync(string userId);
        Task<IEnumerable<ProductComment>> GetUserUnverifiedCommentsAsync(string userId);
        

    }
}
