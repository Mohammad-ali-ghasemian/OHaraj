using OHaraj.Core.Domain.DTOs;
using OHaraj.Core.Domain.Models.Product;

namespace OHaraj.Core.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<CategoryDTO> AddCategory(UpsertCategory input);
        Task<CategoryDTO> UpdateCategory(UpsertCategory input);
        Task<int> DeleteCategory(int CategoryId);
        Task<CategoryDTO> GetCategory(int CategoryId);
        Task<IEnumerable<CategoryDTO>> GetAllCategories(string filter = null);
    }
}
