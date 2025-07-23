using OHaraj.Core.Domain.DTOs;
using OHaraj.Core.Domain.Models.Product;

namespace OHaraj.Core.Interfaces.Services
{
    public interface IProductService
    {
        Task<ProductDTO> AddProduct(UpsertProduct input);
        Task<ProductDTO> UpdateProduct(UpdateProduct input);
        Task<int> DeleteProduct(int productId);
        Task<ProductDTO> GetProduct(int productId);
        Task<IEnumerable<ProductDTO>> GetAllProducts(string filter = null);
        Task<IEnumerable<ProductDTO>> GetProductsByCategory(int categoryId);
        Task<IEnumerable<ProductDTO>> GetProductsByModel(int modelId);

        Task<bool> IsLikedByUser(int productId);
        
        // Comment Section
        Task<CommentDTO> AddComment(UpsertComment input);
        Task<CommentDTO> UpdateComment(UpsertComment input);
        
        Task<CommentDTO> ApproveComment(int commentId);
        
        Task<int> DeleteComment(int commentId);
        Task<int> DeleteAllProductComments(int productId);

        Task<CommentDTO> GetComment(int commentId);

        Task<IEnumerable<CommentDTO>> GetProductAllComments(int productId);
        Task<IEnumerable<CommentDTO>> GetProductVerifiedComments(int productId, int number);
        Task<IEnumerable<CommentDTO>> GetProductUnverifiedComments(int productId);
        Task<IEnumerable<CommentDTO>> GetAllUnverifiedComments();
        Task<IEnumerable<CommentDTO>> GetUserAllComments();
        Task<IEnumerable<CommentDTO>> GetUserUnverifiedComments();
    }
}
