using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OHaraj.Core.Domain.Entities.Configs;
using OHaraj.Core.Domain.Entities.Management;
using OHaraj.Core.Domain.Entities.Settings;
using OHaraj.Core.Domain.Models.Dynamicity;
using OHaraj.Core.Domain.Models.Dynamicity.Configs;
using OHaraj.Core.Enums;
using OHaraj.Core.Interfaces.Repositories;
using OHaraj.Core.Interfaces.Services;
using Project.Application.Contracts.Infrastructure;
using Project.Application.Exceptions;
using System.Xml.Linq;

namespace OHaraj.Services
{
    public class DynamicityService : IDynamicityService
    {
        private readonly IDynamicityRepository _dynamicityRepository;
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string FileContainer;
        private readonly IFileStorageService _uploaderService;
        public DynamicityService(
            IDynamicityRepository dynamicityRepository,
            IAuthenticationRepository authenticationRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IFileStorageService uploaderService
            )
        {
            _dynamicityRepository = dynamicityRepository;
            _authenticationRepository = authenticationRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            FileContainer = "Product";
            _uploaderService = uploaderService;
        }

        public async Task<IdentityUser> Current()
        {
            var userPrincipal = _httpContextAccessor.HttpContext?.User;
            if (userPrincipal == null)
            {
                return null;
            }

            var user = await _authenticationRepository.GetUserByPrincipalAsync(userPrincipal);
            return user;
        }

        public async Task<int> UpsertMenu(UpsertMenu input)
        {
            if (input.ParentId != null && await _dynamicityRepository.GetMenuAsync((int) input.ParentId) == null)
            {
                throw new NotFoundException("منو والد یافت نشد");
            }

            var menu = await _dynamicityRepository.GetMenuAsync(input.Id);
            if (menu == null)
            {
                return await _dynamicityRepository.AddMenuAsync(new Menu
                {
                    Title = input.Title,
                    ParentId = input.ParentId,
                });
            }
            else
            {
                menu.Title = input.Title;
                menu.ParentId = input.ParentId;
                return await _dynamicityRepository.UpdateMenuAsync(menu);
            }
        }

        public async Task<int> DeleteMenu(int menuId)
        {
            var menu = await _dynamicityRepository.GetMenuAsync(menuId);
            if (menu == null)
            {
                throw new NotFoundException("منو یافت نشد!");
            }

            return await _dynamicityRepository.DeleteMenuAsync(menu);
        }

        public async Task<Menu> GetMenu(int menuId)
        {
            var menu = await _dynamicityRepository.GetMenuAsync(menuId);
            if (menu == null)
            {
                throw new NotFoundException("منو یافت نشد!");
            }

            return menu;
        }

        public async Task<IEnumerable<Menu>> GetMenus()
        {
            return await _dynamicityRepository.GetMenusAsync();
        }

        public async Task<IEnumerable<Menu>> GetLoginedUserAccessMenus()
        {
            var user = await Current();
            if (user == null)
            {
                throw new NotFoundException("کاربر یافت است.");
            }

            var accessBanned = await _dynamicityRepository.GetAccessDeniedMenusAsync(await _authenticationRepository.GetUserRolesAsync(user));
            var menus = await _dynamicityRepository.GetMenusAsync();

            return menus.Except(accessBanned);
        }

        public async Task<IEnumerable<Menu>> GetOtherUserAccessMenus(string userId)
        {
            var user = await _authenticationRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException("کاربر یافت نشد.");
            }

            var accessBanned = await _dynamicityRepository.GetAccessDeniedMenusAsync(await _authenticationRepository.GetUserRolesAsync(user));
            var menus = await _dynamicityRepository.GetMenusAsync();

            return menus.Except(accessBanned);
        }

        public async Task<int> UpsertAccessBan(UpsertRoleAccessBanned input)
        {
            if (input.Id == null)
            {
                return await _dynamicityRepository.AddBanAccessAsync(new RoleAccessBanned
                {
                    RoleId = input.RoleId,
                    MenuId = input.MenuId,
                });
            }
            else
            {
                var accessBanned = await _dynamicityRepository.GetBanAccessAsync((int) input.Id);
                if (accessBanned == null)
                {
                    throw new NotFoundException("مورد یافت نشد");
                }
                accessBanned.RoleId = input.RoleId;
                accessBanned.MenuId = input.MenuId;
                return await _dynamicityRepository.UpdateBanAccessAsync(accessBanned);
            }
        }

        public async Task<int> DeleteAccessBan(int accessBanId)
        {
            var accessBanned = await _dynamicityRepository.GetBanAccessAsync(accessBanId);
            if (accessBanned == null)
            {
                throw new NotFoundException("مورد یافت نشد");
            }

            return await _dynamicityRepository.DeleteBanAccessAsync(accessBanned);
        }

        public async Task<RoleAccessBanned> GetAccessBan(int accessBanId)
        {
            var accessBanned = await _dynamicityRepository.GetBanAccessAsync(accessBanId);
            if (accessBanned == null)
            {
                throw new NotFoundException("مورد یافت نشد");
            }

            return accessBanned;
        }

        public async Task<IEnumerable<RoleAccessBanned>> GetAccessBan(string roleId)
        {
            return await _dynamicityRepository.GetBanAccessByRoleAsync(roleId);
        }

        public async Task<IEnumerable<RoleAccessBanned>> GetAllAccessBans()
        {
            return await _dynamicityRepository.GetBanAccessesAsync();
        }

        public async Task<int> UpsertImageConfig(UpsertImageConfig input)
        {
            if (input.Id == null)
            {
                return await _dynamicityRepository.AddImageConfigAsync(new ImageConfigs
                {
                    Name = input.Name,
                    MaxSize = input.MaxSize,
                    MaxWidth = input.MaxWidth,
                    MaxHeight = input.MaxHeight,
                });
            }
            else
            {
                var imageConfig = await _dynamicityRepository.GetImageConfigAsync((int)input.Id);
                if (imageConfig == null)
                {
                    throw new NotFoundException("کانفیگ یافت نشد");
                }
                imageConfig.Name = input.Name;
                imageConfig.MaxSize = input.MaxSize;
                imageConfig.MaxWidth = input.MaxWidth;
                imageConfig.MaxHeight = input.MaxHeight;

                return await _dynamicityRepository.UpdateImageConfigAsync(imageConfig);
            }
        }

        public async Task<int> DeleteImageConfig(int imageConfigId)
        {
            var imageConfig = await _dynamicityRepository.GetImageConfigAsync(imageConfigId);
            if (imageConfig == null)
            {
                throw new NotFoundException("کانفیگ یافت نشد");
            }
            return await _dynamicityRepository.DeleteImageConfigAsync(imageConfig);
        }

        public async Task<ImageConfigs> GetImageConfig(int imageConfigId)
        {
            var imageConfig = await _dynamicityRepository.GetImageConfigAsync(imageConfigId);
            if (imageConfig == null)
            {
                throw new NotFoundException("کانفیگ یافت نشد");
            }
            return imageConfig;
        }

        public async Task<IEnumerable<ImageConfigs>> GetImageConfigs()
        {
            return await _dynamicityRepository.GetImageConfigsAsync();
        }

        public async Task<int> UpsertVideoConfig(UpsertVideoConfig input)
        {
            if (input.Id == null)
            {
                return await _dynamicityRepository.AddVideoConfigAsync(new VideoConfigs
                {
                    Name = input.Name,
                    MaxSize = input.MaxSize,
                    MaxWidth = input.MaxWidth,
                    MaxHeight = input.MaxHeight,
                    MaxLength = input.MaxLength,
                });
            }
            else
            {
                var videoConfig = await _dynamicityRepository.GetVideoConfigAsync((int)input.Id);
                if (videoConfig == null)
                {
                    throw new NotFoundException("کانفیگ یافت نشد");
                }
                videoConfig.Name = input.Name;
                videoConfig.MaxSize = input.MaxSize;
                videoConfig.MaxWidth = input.MaxWidth;
                videoConfig.MaxHeight = input.MaxHeight;
                videoConfig.MaxLength = input.MaxLength;

                return await _dynamicityRepository.UpdateVideoConfigAsync(videoConfig);
            }
        }

        public async Task<int> DeleteVideoConfig(int videoConfigId)
        {
            var videoConfig = await _dynamicityRepository.GetVideoConfigAsync(videoConfigId);
            if (videoConfig == null)
            {
                throw new NotFoundException("کانفیگ یافت نشد");
            }
            return await _dynamicityRepository.DeleteVideoConfigAsync(videoConfig);
        }

        public async Task<VideoConfigs> GetVideoConfig(int videoConfigId)
        {
            var videoConfig = await _dynamicityRepository.GetVideoConfigAsync(videoConfigId);
            if (videoConfig == null)
            {
                throw new NotFoundException("کانفیگ یافت نشد");
            }
            return videoConfig;
        }

        public async Task<IEnumerable<VideoConfigs>> GetVideoConfigs()
        {
            return await _dynamicityRepository.GetVideoConfigsAsync();
        }

        public async Task<int> UpsertAudioConfig(UpsertAudioConfig input)
        {
            if (input.Id == null)
            {
                return await _dynamicityRepository.AddAudioConfigAsync(new AudioConfigs
                {
                    MaxSize = input.MaxSize,
                    MaxLength = input.MaxLength,
                });
            }
            else
            {
                var audioConfig = await _dynamicityRepository.GetAudioConfigAsync((int)input.Id);
                if (audioConfig == null)
                {
                    throw new NotFoundException("کانفیگ یافت نشد");
                }
                audioConfig.MaxSize= input.MaxSize;
                audioConfig.MaxLength = input.MaxLength;

                return await _dynamicityRepository.UpdateAudioConfigAsync(audioConfig);
            }
        }

        public async Task<int> DeleteAudioConfig(int audioConfigId)
        {
            var audioConfig = await _dynamicityRepository.GetAudioConfigAsync(audioConfigId);
            if (audioConfig == null)
            {
                throw new NotFoundException("کانفیگ یافت نشد");
            }
            return await _dynamicityRepository.DeleteAudioConfigAsync(audioConfig);
        }

        public async Task<AudioConfigs> GetAudioConfig(int audioConfigId)
        {
            var audioConfig = await _dynamicityRepository.GetAudioConfigAsync(audioConfigId);
            if (audioConfig == null)
            {
                throw new NotFoundException("کانفیگ یافت نشد");
            }
            return audioConfig;
        }

        public async Task<IEnumerable<AudioConfigs>> GetAudioConfigs()
        {
            return await _dynamicityRepository.GetAudioConfigsAsync();
        }

        public Task<IEnumerable<DocumentSettings>> GetDocumentSettings()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DocumentSettings>> GetDocumentSettingsByArea(Area area)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DocumentSettings>> GetDocumentSettingsByConfigId(int documentConfigId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DocumentSettings>> GetDocumentSettingsByMenuId(int menuId)
        {
            throw new NotImplementedException();
        }

        public Task<ImageConfigs> GetImageConfig(int imageConfigId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ImageConfigs>> GetImageConfigs()
        {
            throw new NotImplementedException();
        }

        public Task<ImageSettings> GetImageSetting(int imageSettingId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ImageSettings>> GetImageSettings()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ImageSettings>> GetImageSettingsByArea(Area area)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ImageSettings>> GetImageSettingsByConfigId(int imageConfigId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ImageSettings>> GetImageSettingsByMenuId(int menuId)
        {
            throw new NotImplementedException();
        }

        public Task<Menu> GetMenu(int menuId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Menu>> GetMenus()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Menu>> GetMyAccessMenus()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Menu>> GetOtherAccessMenus(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<VideoConfigs> GetVideoConfig(int audioConfigId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VideoConfigs>> GetVideoConfigs()
        {
            throw new NotImplementedException();
        }

        public Task<VideoSettings> GetVideoSetting(int videoSettingId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VideoSettings>> GetVideoSettings()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VideoSettings>> GetVideoSettingsByArea(Area area)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VideoSettings>> GetVideoSettingsByConfigId(int videoConfigId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VideoSettings>> GetVideoSettingsByMenuId(int menuId)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpsertAccessBan(UpsertRoleAccessBanned input)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpsertAudioConfig(UpsertAudioConfig input)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpsertAudioSetting(UpsertSetting input)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpsertDocumentConfig(UpsertDocumentConfig input)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpsertDocumentSetting(UpsertSetting input)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpsertImageConfig(UpsertImageConfig input)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpsertImageSetting(UpsertSetting input)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpsertMenu(UpsertMenu input)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpsertVideoConfig(UpsertVideoConfig input)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpsertVideoSetting(UpsertSetting input)
        {
            throw new NotImplementedException();
        }
    }
}
