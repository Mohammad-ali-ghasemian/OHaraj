using Microsoft.EntityFrameworkCore;
using OHaraj.Core.Domain.Entities.Management;
using OHaraj.Core.Domain.Entities.Shop;
using OHaraj.Core.Interfaces.Repositories;
using OHaraj.Infrastructure;

namespace OHaraj.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddCategoryAsync(Category input)
        {
            await _dbContext.AddAsync(input);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category input)
        {
            _dbContext.Update(input);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(Category input)
        {
            _dbContext.Remove(input);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            return await _dbContext.Categories.AsNoTracking()
                .Include(nameof(Category.ParentCategory))
                .Include(nameof(Category.SubCategories))
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            return await _dbContext.Categories.AsNoTracking()
                .Include(nameof(Category.ParentCategory))
                .Include(nameof(Category.SubCategories))
                .ToListAsync();
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
