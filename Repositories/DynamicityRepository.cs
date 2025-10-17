using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OHaraj.Core.Domain.Entities.Configs;
using OHaraj.Core.Domain.Entities.Management;
using OHaraj.Core.Domain.Entities.Settings;
using OHaraj.Core.Enums;
using OHaraj.Core.Interfaces.Repositories;
using OHaraj.Infrastructure;

namespace OHaraj.Repositories
{
    public class DynamicityRepository : IDynamicityRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DynamicityRepository(ApplicationDbContext dbcontext, RoleManager<IdentityRole> roleManager) {
            _dbcontext = dbcontext;
            _roleManager = roleManager;
        }
        //Menu
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

        public async Task<IEnumerable<Menu>> GetAccessMenusAsync(IEnumerable<string> roleIds)
        {
            var access = await _dbcontext.RoleAccess.AsNoTracking()
                .Where(x => roleIds.Contains(x.RoleId))
                .Select(x => x.MenuId)
                .ToListAsync();

            return await _dbcontext.Menus.AsNoTracking()
                .Include(nameof(Menu))
                .Where(x => access.Contains(x.Id))
                .ToListAsync();
        }



        //Role Access
        public async Task<int> AddAccessAsync(RoleAccess input)
        {
            await _dbcontext.AddAsync(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }

        public async Task<int> UpdateAccessAsync(RoleAccess input)
        {
            _dbcontext.Update(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }

        public async Task<int> DeleteAccessAsync(RoleAccess input)
        {
            _dbcontext.Remove(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }


        public async Task<RoleAccess> GetAccessAsync(int id)
        {
            return await _dbcontext.RoleAccess
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<RoleAccess>> GetAccessesAsync()
        {
            return await _dbcontext.RoleAccess
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<RoleAccess>> GetAccessByRoleAsync(string roleId)
        {
            return await _dbcontext.RoleAccess
                .AsNoTracking()
                .Where(x => x.RoleId == roleId)
                .ToListAsync();
        }




        //Configs
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

        public async Task<int> DeleteDocumentConfigAsync(DocumentConfigs input)
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




        //Settings
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

        public async Task<int> DeleteImageSettingAsync(ImageSettings input)
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

        public async Task<IEnumerable<AudioSettings>> GetAudioSettingsByAreaAsync(Area area)
        {
            return await _dbcontext.AudioSettings
                .AsNoTracking()
                .Where(x => x.Area == area)
                .ToListAsync();
        }

        public async Task<IEnumerable<AudioSettings>> GetAudioSettingsByConfigIdAsync(int configId)
        {
            return await _dbcontext.AudioSettings
                .AsNoTracking()
                .Where(x => x.AudioConfigsId == configId)
                .ToListAsync();
        }



        public async Task<int> AddDocumentSettingAsync(DocumentSettings input)
        {
            await _dbcontext.AddAsync(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }

        public async Task<int> UpdateDocumentSettingAsync(DocumentSettings input)
        {
            _dbcontext.Update(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }

        public async Task<int> DeleteDocumentSettingAsync(DocumentSettings input)
        {
            _dbcontext.Remove(input);
            await _dbcontext.SaveChangesAsync();
            return input.Id;
        }


        public async Task<DocumentSettings> GetDocumentSettingAsync(int id)
        {
            return await _dbcontext.DocumentSettings
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<DocumentSettings>> GetDocumentSettingsAsync()
        {
            return await _dbcontext.DocumentSettings
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<DocumentSettings>> GetDocumentSettingsByMenuIdAsync(int menuId)
        {
            return await _dbcontext.DocumentSettings
                .AsNoTracking()
                .Where(x => x.MenuId == menuId)
                .ToListAsync();
        }

        public async Task<IEnumerable<DocumentSettings>> GetDocumentSettingsByAreaAsync(Area area)
        {
            return await _dbcontext.DocumentSettings
                .AsNoTracking()
                .Where(x => x.Area == area)
                .ToListAsync();
        }

        public async Task<IEnumerable<DocumentSettings>> GetDocumentSettingsByConfigIdAsync(int configId)
        {
            return await _dbcontext.DocumentSettings
                .AsNoTracking()
                .Where(x => x.DocumentConfigsId == configId)
                .ToListAsync();
        }

        public async Task<IdentityResult> AddRoleAsync(string roleName)
        {
            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                return await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
            else
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "DuplicateRole",
                    Description = "A role with the name 'Admin' already exists."
                });
            }
        }

        public Task<IdentityResult> UpdateRoleAsync(string roleName)
        {
            
        }

        public Task<IdentityResult> DeleteRoleAsync(string roleName)
        {
            
        }

        public Task<IdentityRole> GetRoleAsync(string roleId)
        {
            
        }

        public Task<IEnumerable<IdentityRole>> GetRolesAsync(string userId)
        {
            
        }

        public Task<IEnumerable<IdentityRole>> GetRolesAsync()
        {
            
        }

        public Task<IEnumerable<IdentityRole>> GiveRolesAsync(string userId, IEnumerable<IdentityRole> roles)
        {
            
        }

        public Task<IEnumerable<IdentityRole>> TakeRolesAsync(string userId, IEnumerable<IdentityRole> roles)
        {
            
        }
    }
}
