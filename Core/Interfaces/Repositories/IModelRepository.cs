using OHaraj.Core.Domain.Entities.Management;
using OHaraj.Core.Domain.Entities.Shop;

namespace OHaraj.Core.Interfaces.Repositories
{
    public interface IModelRepository
    {
        Task AddModelAsync(Model input);
        Task UpdateModelAsync(Model input);
        Task DeleteModelAsync(Model input);

        Task<Model> GetModelAsync(int id);
        Task<IEnumerable<Model>> GetModelsAsync();

        Task<int> AddFileToTableAsync(FileManagement input);
        Task<int> UpdateFileToTableAsync(FileManagement input);
        Task<FileManagement> GetFileToTableAsync(int? fileId);
        Task<int> DeleteFileToTableAsync(FileManagement input);
    }
}
