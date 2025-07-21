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

        Task<>
        // add/get/delete/update comment
        //approve comment
        //delete all product comments
        // get product verified comments (number)
        // get product unverified comments
        // get product all comments
        // get all unverified comments
        // GetUserAllCommentsAsync
        // GetUserUnverifiedCommentsAsync
    }
}
