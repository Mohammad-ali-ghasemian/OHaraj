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

        //Menu
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



        //Role Access Banned
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



        //Configs
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



        public async Task<int> UpsertDocumentConfig(UpsertDocumentConfig input)
        {
            if (input.Id == null)
            {
                return await _dynamicityRepository.AddDocumentConfigAsync(new DocumentConfigs
                {
                    MaxSize = input.MaxSize,
                });
            }
            else
            {
                var documentConfig = await _dynamicityRepository.GetDocumentConfigAsync((int)input.Id);
                if (documentConfig == null)
                {
                    throw new NotFoundException("کانفیگ یافت نشد");
                }
                documentConfig.MaxSize = input.MaxSize;

                return await _dynamicityRepository.UpdateDocumentConfigAsync(documentConfig);
            }
        }

        public async Task<int> DeleteDocumentConfig(int documentConfigId)
        {
            var documentConfig = await _dynamicityRepository.GetDocumentConfigAsync(documentConfigId);
            if (documentConfig == null)
            {
                throw new NotFoundException("کانفیگ یافت نشد");
            }
            return await _dynamicityRepository.DeleteDocumentConfigAsync(documentConfig);
        }

        public async Task<DocumentConfigs> GetDocumentConfig(int documentConfigId)
        {
            var documentConfig = await _dynamicityRepository.GetDocumentConfigAsync(documentConfigId);
            if (documentConfig == null)
            {
                throw new NotFoundException("کانفیگ یافت نشد");
            }
            return documentConfig;
        }

        public async Task<IEnumerable<DocumentConfigs>> GetDocumentConfigs()
        {
            return await _dynamicityRepository.GetDocumentConfigsAsync();
        }



        //Settings
        public async Task<int> UpsertImageSetting(UpsertSetting input)
        {
            if (input.Id == null)
            {
                return await _dynamicityRepository.AddImageSettingAsync(new ImageSettings
                {
                    Area = input.Area,
                    ImageConfigsId = input.ConfigId,
                    MenuId = input.MenuId,
                });
            }
            else
            {
                var imageSetting = await _dynamicityRepository.GetImageSettingAsync((int) input.Id);
                if (imageSetting == null)
                {
                    throw new NotFoundException("تنظیمات یافت نشد");
                }
                imageSetting.Area = input.Area;
                imageSetting.ImageConfigsId = input.ConfigId;
                imageSetting.MenuId = input.MenuId;

                return await _dynamicityRepository.UpdateImageSettingAsync(imageSetting);
            }
        }

        public async Task<int> DeleteImageSetting(int imageSettingId)
        {
            var imageSetting = await _dynamicityRepository.GetImageSettingAsync(imageSettingId);
            if (imageSetting == null)
            {
                throw new NotFoundException("تنظیمات یافت نشد");
            }

            return await _dynamicityRepository.DeleteImageSettingAsync(imageSetting);
        }

        public async Task<ImageSettings> GetImageSetting(int imageSettingId)
        {
            var imageSetting = await _dynamicityRepository.GetImageSettingAsync(imageSettingId);
            if (imageSetting == null)
            {
                throw new NotFoundException("تنظیمات یافت نشد");
            }

            return imageSetting;
        }

        public async Task<IEnumerable<ImageSettings>> GetImageSettings()
        {
            return await _dynamicityRepository.GetImageSettingsAsync();
        }

        public async Task<IEnumerable<ImageSettings>> GetImageSettingsByMenuId(int menuId)
        {
            if (await _dynamicityRepository.GetMenuAsync(menuId) == null)
            {
                throw new NotFoundException("منو یافت نشد");
            }
            return await _dynamicityRepository.GetImageSettingsByMenuIdAsync(menuId);
        }

        public async Task<IEnumerable<ImageSettings>> GetImageSettingsByArea(Area area)
        {
            return await _dynamicityRepository.GetImageSettingsByAreaAsync(area);
        }

        public async Task<IEnumerable<ImageSettings>> GetImageSettingsByConfigId(int imageConfigId)
        {
            if (await _dynamicityRepository.GetImageConfigAsync(imageConfigId) == null)
            {
                throw new NotFoundException("کانفیگ یافت نشد");
            }

            return await _dynamicityRepository.GetImageSettingsByConfigIdAsync(imageConfigId);
        }



        public async Task<int> UpsertVideoSetting(UpsertSetting input)
        {
            if (input.Id == null)
            {
                return await _dynamicityRepository.AddVideoSettingAsync(new VideoSettings
                {
                    Area = input.Area,
                    VideoConfigsId = input.ConfigId,
                    MenuId = input.MenuId,
                });
            }
            else
            {
                var videoSetting = await _dynamicityRepository.GetVideoSettingAsync((int)input.Id);
                if (videoSetting == null)
                {
                    throw new NotFoundException("تنظیمات یافت نشد");
                }
                videoSetting.Area = input.Area;
                videoSetting.VideoConfigsId = input.ConfigId;
                videoSetting.MenuId = input.MenuId;

                return await _dynamicityRepository.UpdateVideoSettingAsync(videoSetting);
            }
        }

        public async Task<int> DeleteVideoSetting(int videoSettingId)
        {
            var videoSetting = await _dynamicityRepository.GetVideoSettingAsync(videoSettingId);
            if (videoSetting == null)
            {
                throw new NotFoundException("تنظیمات یافت نشد");
            }

            return await _dynamicityRepository.DeleteVideoSettingAsync(videoSetting);
        }

        public async Task<VideoSettings> GetVideoSetting(int videoSettingId)
        {
            var videoSetting = await _dynamicityRepository.GetVideoSettingAsync(videoSettingId);
            if (videoSetting == null)
            {
                throw new NotFoundException("تنظیمات یافت نشد");
            }

            return videoSetting;
        }

        public async Task<IEnumerable<VideoSettings>> GetVideoSettings()
        {
            return await _dynamicityRepository.GetVideoSettingsAsync();
        }

        public async Task<IEnumerable<VideoSettings>> GetVideoSettingsByMenuId(int menuId)
        {
            if (await _dynamicityRepository.GetMenuAsync(menuId) == null)
            {
                throw new NotFoundException("منو یافت نشد");
            }
            return await _dynamicityRepository.GetVideoSettingsByMenuIdAsync(menuId);
        }

        public async Task<IEnumerable<VideoSettings>> GetVideoSettingsByArea(Area area)
        {
            return await _dynamicityRepository.GetVideoSettingsByAreaAsync(area);
        }

        public async Task<IEnumerable<VideoSettings>> GetVideoSettingsByConfigId(int videoConfigId)
        {
            if (await _dynamicityRepository.GetVideoConfigAsync(videoConfigId) == null)
            {
                throw new NotFoundException("کانفیگ یافت نشد");
            }

            return await _dynamicityRepository.GetVideoSettingsByConfigIdAsync(videoConfigId);
        }



        public async Task<int> UpsertAudioSetting(UpsertSetting input)
        {
            if (input.Id == null)
            {
                return await _dynamicityRepository.AddAudioSettingAsync(new AudioSettings
                {
                    Area = input.Area,
                    AudioConfigsId = input.ConfigId,
                    MenuId = input.MenuId,
                });
            }
            else
            {
                var audioSetting = await _dynamicityRepository.GetAudioSettingAsync((int)input.Id);
                if (audioSetting == null)
                {
                    throw new NotFoundException("تنظیمات یافت نشد");
                }
                audioSetting.Area = input.Area;
                audioSetting.AudioConfigsId = input.ConfigId;
                audioSetting.MenuId = input.MenuId;

                return await _dynamicityRepository.UpdateAudioSettingAsync(audioSetting);
            }
        }

        public async Task<int> DeleteAudioSetting(int audioSettingId)
        {
            var audioSetting = await _dynamicityRepository.GetAudioSettingAsync(audioSettingId);
            if (audioSetting == null)
            {
                throw new NotFoundException("تنظیمات یافت نشد");
            }

            return await _dynamicityRepository.DeleteAudioSettingAsync(audioSetting);
        }

        public async Task<AudioSettings> GetAudioSetting(int audioSettingId)
        {
            var audioSetting = await _dynamicityRepository.GetAudioSettingAsync(audioSettingId);
            if (audioSetting == null)
            {
                throw new NotFoundException("تنظیمات یافت نشد");
            }

            return audioSetting;
        }

        public async Task<IEnumerable<AudioSettings>> GetAudioSettings()
        {
            return await _dynamicityRepository.GetAudioSettingsAsync();
        }

        public async Task<IEnumerable<AudioSettings>> GetAudioSettingsByMenuId(int menuId)
        {
            if (await _dynamicityRepository.GetMenuAsync(menuId) == null)
            {
                throw new NotFoundException("منو یافت نشد");
            }
            return await _dynamicityRepository.GetAudioSettingsByMenuIdAsync(menuId);
        }

        public async Task<IEnumerable<AudioSettings>> GetAudioSettingsByArea(Area area)
        {
            return await _dynamicityRepository.GetAudioSettingsByAreaAsync(area);
        }

        public async Task<IEnumerable<AudioSettings>> GetAudioSettingsByConfigId(int audioConfigId)
        {
            if (await _dynamicityRepository.GetAudioConfigAsync(audioConfigId) == null)
            {
                throw new NotFoundException("کانفیگ یافت نشد");
            }

            return await _dynamicityRepository.GetAudioSettingsByConfigIdAsync(audioConfigId);
        }



        public Task<int> UpsertDocumentConfig(UpsertDocumentConfig input)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpsertDocumentSetting(UpsertSetting input)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpsertVideoConfig(UpsertImageConfig input)
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
