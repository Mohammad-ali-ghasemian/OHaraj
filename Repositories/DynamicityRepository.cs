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

        public async Task<int> UpdateVideoConfigAsync(VideoConfigs input)
        {
            _dbcontext.Update(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }

        public async Task<int> DeleteVideoConfigAsync(VideoConfigs input)
        {
            _dbcontext.Remove(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }

        public async Task<VideoConfigs> GetVideoConfigAsync(int id)
        {
            return await _dbcontext.VideoConfigs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<VideoConfigs>> GetVideoConfigsAsync()
        {
            return await _dbcontext.VideoConfigs
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> AddAudioConfigAsync(AudioConfigs input)
        {
            await _dbcontext.AddAsync(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }

        public async Task<int> UpdateAudioConfigAsync(AudioConfigs input)
        {
            _dbcontext.Update(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }

        public async Task<int> DeleteAudioConfigAsync(AudioConfigs input)
        {
            _dbcontext.Remove(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }

        public async Task<AudioConfigs> GetAudioConfigAsync(int id)
        {
            return await _dbcontext.AudioConfigs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<AudioConfigs>> GetAudioConfigsAsync()
        {
            return await _dbcontext.AudioConfigs
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> AddDocumentConfigAsync(DocumentConfigs input)
        {
            await _dbcontext.AddAsync(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }

        public async Task<int> UpdateDocumentConfigAsync(DocumentConfigs input)
        {
            _dbcontext.Update(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }

        public async Task<int> RemoveDocumentConfigAsync(DocumentConfigs input)
        {
            _dbcontext.Remove(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }

        public async Task<DocumentConfigs> GetDocumentConfigAsync(int id)
        {
            return await _dbcontext.DocumentConfigs
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<DocumentConfigs>> GetDocumentConfigsAsync()
        {
            return await _dbcontext.DocumentConfigs
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> AddImageSettingAsync(ImageSettings input)
        {
            await _dbcontext.AddAsync(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }

        public async Task<int> UpdateImageSettingAsync(ImageSettings input)
        {
            _dbcontext.Update(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }

        public async Task<int> RemoveImageSettingAsync(ImageSettings input)
        {
            _dbcontext.Remove(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }

        public async Task<ImageSettings> GetImageSettingAsync(int id)
        {
            return await _dbcontext.ImageSettings
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<ImageSettings>> GetImageSettingsAsync()
        {
            return await _dbcontext.ImageSettings.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<ImageSettings>> GetImageSettingsByMenuIdAsync(int menuId)
        {
            return await _dbcontext.ImageSettings
                .AsNoTracking()
                .Where(x => x.MenuId == menuId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ImageSettings>> GetImageSettingsByAreaAsync(Area area)
        {
            return await _dbcontext.ImageSettings
                .AsNoTracking()
                .Where(x => x.Area == area)
                .ToListAsync();
        }

        public async Task<IEnumerable<ImageSettings>> GetImageSettingsByConfigIdAsync(int configId)
        {
            return await _dbcontext.ImageSettings
                .AsNoTracking()
                .Where(x => x.ImageConfigsId == configId)
                .ToListAsync();
        }

        public async Task<int> AddVideoSettingAsync(VideoSettings input)
        {
            await _dbcontext.AddAsync(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }

        public async Task<int> UpdateVideoSettingAsync(VideoSettings input)
        {
            _dbcontext.Update(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }

        public async Task<int> DeleteVideoSettingAsync(VideoSettings input)
        {
            _dbcontext.Remove(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }

        public async Task<VideoSettings> GetVideoSettingAsync(int id)
        {
            return await _dbcontext.VideoSettings
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<VideoSettings>> GetVideoSettingsAsync()
        {
            return await _dbcontext.VideoSettings
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<VideoSettings>> GetVideoSettingsByMenuIdAsync(int menuId)
        {
            return await _dbcontext.VideoSettings
                .AsNoTracking()
                .Where(x => x.MenuId == menuId)
                .ToListAsync();
        }

        public async Task<IEnumerable<VideoSettings>> GetVideoSettingsByAreaAsync(Area area)
        {
            return await _dbcontext.VideoSettings
                .AsNoTracking()
                .Where(x => x.Area == area)
                .ToListAsync();
        }

        public async Task<IEnumerable<VideoSettings>> GetVideoSettingsByConfigIdAsync(int configId)
        {
            return await _dbcontext.VideoSettings
                .AsNoTracking()
                .Where(x => x.VideoConfigsId == configId)
                .ToListAsync();
        }

        public async Task<int> AddAudioSettingAsync(AudioSettings input)
        {
            await _dbcontext.AddAsync(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }

        public async Task<int> UpdateAudioSettingAsync(AudioSettings input)
        {
            _dbcontext.Update(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }

        public async Task<int> DeleteAudioSettingAsync(AudioSettings input)
        {
            _dbcontext.Remove(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }

        public async Task<AudioSettings> GetAudioSettingAsync(int id)
        {
            return await _dbcontext.AudioSettings
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<AudioSettings>> GetAudioSettingsAsync()
        {
            return await _dbcontext.AudioSettings
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<AudioSettings>> GetAudioSettingsByMenuIdAsync(int menuId)
        {
            return await _dbcontext.AudioSettings
                .AsNoTracking()
                .Where(x => x.MenuId == menuId)
                .ToListAsync();
        }

        public Task<IEnumerable<AudioSettings>> GetAudioSettingsByAreaAsync(Area area)
        {
            
        }

        public Task<IEnumerable<AudioSettings>> GetAudioSettingsByConfigIdAsync(int configId)
        {
            
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
