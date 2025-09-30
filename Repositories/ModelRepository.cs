using Microsoft.EntityFrameworkCore;
using OHaraj.Core.Domain.Entities.Handling;
using OHaraj.Core.Domain.Entities.Shop;
using OHaraj.Core.Interfaces.Repositories;
using OHaraj.Infrastructure;

namespace OHaraj.Repositories
{
    public class ModelRepository : IModelRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ModelRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext; 
        }

        public async Task AddModelAsync(Model input)
        {
            await _dbContext.AddAsync(input);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateModelAsync(Model input)
        {
            _dbContext.Update(input);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteModelAsync(Model input)
        {
            _dbContext.Remove(input);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Model> GetModelAsync(int id)
        {
            return await _dbContext.Models.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Model>> GetModelsAsync()
        {
            return await _dbContext.Models.AsNoTracking().ToListAsync();
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
    }
}
