using OHaraj.Core.Domain.DTOs;
using OHaraj.Core.Domain.Models.Product;

namespace OHaraj.Core.Interfaces.Services
{
    public interface IModelService
    {
        Task<ModelDTO> AddModel(UpsertModel input);
        Task<ModelDTO> UpdateModel(UpsertModel input);
        Task<int> DeleteModel(int modelId);
        Task<ModelDTO> GetModel(int modelId);
        Task<IEnumerable<ModelDTO>> GetAllModels();
    }
}
