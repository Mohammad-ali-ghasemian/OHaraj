using Microsoft.EntityFrameworkCore;
using OHaraj.Core.Domain.Entities.Configs;
using OHaraj.Core.Domain.Entities.Handling;
using OHaraj.Core.Domain.Entities.Settings;
using OHaraj.Core.Enums;
using OHaraj.Core.Interfaces.Repositories;
using OHaraj.Infrastructure;

namespace OHaraj.Repositories
{
    public class DynamicityRepository : IDynamicityRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public DynamicityRepository(ApplicationDbContext dbcontext) {
            _dbcontext = dbcontext;
        }
        public async Task<int> AddMenuAsync(Menu input)
        {
            await _dbcontext.AddAsync(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }

        public async Task<int> UpdateMenuAsync(Menu input)
        {
            _dbcontext.Update(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }

        public async Task<int> DeleteMenuAsync(Menu input)
        {
            _dbcontext.Remove(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }

        public async Task<Menu> GetMenuAsync(int id)
        {
            return await _dbcontext.Menus.AsNoTracking()
                .Include(nameof(Menu))
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Menu>> GetMenusAsync()
        {
            return await _dbcontext.Menus.AsNoTracking().Include(nameof(Menu)).ToListAsync();
        }

        public async Task<IEnumerable<Menu>> GetAccessDeniedMenusAsync(IEnumerable<string> roleIds)
        {
            var accessBanned = await _dbcontext.RoleAccessBanned.AsNoTracking()
                .Where(x => roleIds.Contains(x.RoleId))
                .Select(x => x.MenuId)
                .ToListAsync();

            return await _dbcontext.Menus.AsNoTracking()
                .Include(nameof(Menu))
                .Where(x => accessBanned.Contains(x.Id))
                .ToListAsync();
        }

        public async Task<int> AddBanAccessAsync(RoleAccessBanned input)
        {
            await _dbcontext.AddAsync(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }

        public async Task<int> UpdateBanAccessAsync(RoleAccessBanned input)
        {
            _dbcontext.Update(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }

        public async Task<int> DeleteBanAccessAsync(RoleAccessBanned input)
        {
            _dbcontext.Remove(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }

        public async Task<RoleAccessBanned> GetBanAccessAsync(int id)
        {
            return await _dbcontext.RoleAccessBanned
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<RoleAccessBanned>> GetBanAccessesAsync()
        {
            return await _dbcontext.RoleAccessBanned
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<RoleAccessBanned>> GetBanAccessByRoleAsync(string roleId)
        {
            return await _dbcontext.RoleAccessBanned
                .AsNoTracking()
                .Where(x => x.RoleId == roleId)
                .ToListAsync();
        }

        public async Task<int> AddImageConfigAsync(ImageConfigs input)
        {
            await _dbcontext.AddAsync(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }

        public async Task<int> UpdateImageConfigAsync(ImageConfigs input)
        {
            _dbcontext.Update(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }

        public async Task<int> DeleteImageConfigAsync(ImageConfigs input)
        {
            _dbcontext.Remove(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }

        public async Task<ImageConfigs> GetImageConfigAsync(int id)
        {
            return await _dbcontext.ImageConfigs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<ImageConfigs>> GetImageConfigsAsync()
        {
            return await _dbcontext.ImageConfigs
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> AddVideoConfigAsync(VideoConfigs input)
        {
            await _dbcontext.AddAsync(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }

        public Task<int> UpdateVideoConfigAsync(VideoConfigs input)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteVideoConfigAsync(VideoConfigs input)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Menu>> GetAccessDeniedMenusAsync(IEnumerable<string> roleIds)
        {
            throw new NotImplementedException();
        }

        public Task<AudioConfigs> GetAudioConfigAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AudioConfigs>> GetAudioConfigsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AudioSettings> GetAudioSettingAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AudioSettings>> GetAudioSettingsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AudioSettings>> GetAudioSettingsByAreaAsync(Area area)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AudioSettings>> GetAudioSettingsByConfigIdAsync(int configId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AudioSettings>> GetAudioSettingsByMenuIdAsync(int menuId)
        {
            throw new NotImplementedException();
        }

        public Task<RoleAccessBanned> GetBanAccessAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RoleAccessBanned>> GetBanAccessByRoleAsync(int roleId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RoleAccessBanned>> GetBanAccessesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DocumentConfigs> GetDocumentConfigAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DocumentConfigs>> GetDocumentConfigsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DocumentSettings> GetDocumentSettingAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DocumentSettings>> GetDocumentSettingsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DocumentSettings>> GetDocumentSettingsByAreaAsync(Area area)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DocumentSettings>> GetDocumentSettingsByConfigIdAsync(int configId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DocumentSettings>> GetDocumentSettingsByMenuIdAsync(int menuId)
        {
            throw new NotImplementedException();
        }

        public Task<ImageConfigs> GetImageConfigAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ImageConfigs>> GetImageConfigsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ImageSettings> GetImageSettingAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ImageSettings>> GetImageSettingsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ImageSettings>> GetImageSettingsByAreaAsync(Area area)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ImageSettings>> GetImageSettingsByConfigIdAsync(int configId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ImageSettings>> GetImageSettingsByMenuIdAsync(int menuId)
        {
            throw new NotImplementedException();
        }

        public Task<Menu> GetMenuAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Menu>> GetMenusAsync()
        {
            throw new NotImplementedException();
        }

        public Task<VideoConfigs> GetVideoConfigAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VideoConfigs>> GetVideoConfigsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<VideoSettings> GetVideoSettingAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VideoSettings>> GetVideoSettingsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VideoSettings>> GetVideoSettingsByAreaAsync(Area area)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VideoSettings>> GetVideoSettingsByConfigIdAsync(int configId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VideoSettings>> GetVideoSettingsByMenuIdAsync(int menuId)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAudioConfigAsync(AudioConfigs input)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAudioSettingAsync(AudioSettings input)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateBanAccessAsync(RoleAccessBanned input)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateDocumentConfigAsync(DocumentConfigs input)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateDocumentSettingAsync(DocumentSettings input)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateImageConfigAsync(ImageConfigs input)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateImageSettingAsync(ImageSettings input)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateMenuAsync(Menu input)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateVideoConfigAsync(VideoConfigs input)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateVideoSettingAsync(VideoSettings input)
        {
            throw new NotImplementedException();
        }
    }
}
