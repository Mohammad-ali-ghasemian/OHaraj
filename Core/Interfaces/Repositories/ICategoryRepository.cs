using OHaraj.Core.Domain.Entities.Management;
using OHaraj.Core.Domain.Entities.Shop;

namespace OHaraj.Core.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task AddCategoryAsync(Category input);
        Task UpdateCategoryAsync(Category input);
        Task DeleteCategoryAsync(Category input);

        Task<Category> GetCategoryAsync(int id);
        Task<IEnumerable<Category>> GetCategoriesAsync();

        Task<int> AddFileToTableAsync(FileManagement input);
        Task<int> UpdateFileToTableAsync(FileManagement input);
        Task<FileManagement> GetFileToTableAsync(int? fileId);
        Task<int> DeleteFileToTableAsync(FileManagement input);
    }
}
