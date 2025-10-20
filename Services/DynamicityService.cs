using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OHaraj.Core.Domain.DTOs;
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

        public async Task<bool> HasCurrentUserAccess(int menuId)
        {
            var user = await Current();
            // if the user did not login, check if the menu id is in the anonymous menus
            if (user == null)
            {
                var anonymousMenus = await GetAnonymousUserAccessMenus();
                if (anonymousMenus.Any(x => x.Id == menuId))
                {
                    return true;
                }

                throw new UnauthorizedAccessException("دسترسی غیرمجاز");
            }

            var menu = await (_dynamicityRepository.GetMenuAsync(menuId));
            if (menu == null)
            {
                throw new NotFoundException("منو یافت نشد");
            }

            var roleNames = await _authenticationRepository.GetUserRolesAsync(user);
            List<string> roleIds = new List<string>();

            IdentityRole role;
            foreach (string roleName in roleNames)
            {
                role = await _dynamicityRepository.GetRoleByNameAsync(roleName);
                roleIds.Add(role.Id);
            }

            // found current user's role ids. now let us see with these roles can access the menu or not.
            var accessMenus = await _dynamicityRepository.GetAccessMenusAsync(roleIds);
            if (accessMenus.Any(m => m.Id == menuId))
            {
                return true;
            }
            else
            {
                throw new UnauthorizedAccessException("دسترسی غیرمجاز");
            }

        }

        public async Task<IEnumerable<Menu>> GetAnonymousUserAccessMenus()
        {
            var allMenus = await _dynamicityRepository.GetMenusAsync();
            //var allRoles = await _authenticationRepository.GetRolesAsync();

            //List<IdentityRole> roleMenus = new List<IdentityRole>();
            //foreach (var role in allRoles)
            //{
            //    roleMenus.Add(await _dynamicityRepository.)
            //}

            var accessMenus = await _dynamicityRepository.GetAccessesAsync();

            return allMenus.Where(menu => !accessMenus.Any(x => x.MenuId == menu.Id));
        }

        public async Task<IEnumerable<Menu>> GetLoginedUserAccessMenus()
        {
            var user = await Current();
            if (user == null)
            {
                throw new NotFoundException("کاربر یافت نشد.");
            }

            var roleNames = await _authenticationRepository.GetUserRolesAsync(user);
            List<string> roleIds = new List<string>();

            IdentityRole role;
            foreach (string roleName in roleNames)
            {
                role = await _dynamicityRepository.GetRoleByNameAsync(roleName);
                roleIds.Add(role.Id);
            }

            var access = await _dynamicityRepository.GetAccessMenusAsync(roleIds);
            //var menus = await _dynamicityRepository.GetMenusAsync();

            //return menus.Except(accessBanned);
            return access;
        }

        public async Task<IEnumerable<Menu>> GetOtherUserAccessMenus(string userId)
        {
            var user = await _authenticationRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException("کاربر یافت نشد.");
            }

            var roleNames = await _authenticationRepository.GetUserRolesAsync(user);
            List<string> roleIds = new List<string>();

            IdentityRole role;
            foreach (string roleName in roleNames)
            {
                role = await _dynamicityRepository.GetRoleByNameAsync(roleName);
                roleIds.Add(role.Id);
            }

            var access = await _dynamicityRepository.GetAccessMenusAsync(roleIds);
            //var menus = await _dynamicityRepository.GetMenusAsync();

            //return menus.Except(accessBanned);
            return access;
        }



        //Role Access 
        public async Task<int> UpsertAccess(UpsertRoleAccess input)
        {
            if (await _dynamicityRepository.GetMenuAsync(input.MenuId) == null)
            {
                throw new NotFoundException("منو یافت نشد");
            }
            if (await _dynamicityRepository.GetRoleByIdAsync(input.RoleId) == null)
            {
                throw new NotFoundException("رول یافت نشد");
            }

            if (input.Id == null)
            {
                return await _dynamicityRepository.AddAccessAsync(new RoleAccess
                {
                    RoleId = input.RoleId,
                    MenuId = input.MenuId,
                });
            }
            else
            {
                var access = await _dynamicityRepository.GetAccessAsync((int) input.Id);
                if (access == null)
                {
                    throw new NotFoundException("مورد یافت نشد");
                }
                access.RoleId = input.RoleId;
                access.MenuId = input.MenuId;
                return await _dynamicityRepository.UpdateAccessAsync(access);
            }
        }

        public async Task<int> DeleteAccess(int accessId)
        {
            var access = await _dynamicityRepository.GetAccessAsync(accessId);
            if (access == null)
            {
                throw new NotFoundException("مورد یافت نشد");
            }

            return await _dynamicityRepository.DeleteAccessAsync(access);
        }

        public async Task<RoleAccess> GetAccess(int accessId)
        {
            var access = await _dynamicityRepository.GetAccessAsync(accessId);
            if (access == null)
            {
                throw new NotFoundException("مورد یافت نشد");
            }

            return access;
        }

        public async Task<IEnumerable<RoleAccess>> GetAccess(string roleId)
        {
            return await _dynamicityRepository.GetAccessByRoleAsync(roleId);
        }

        public async Task<IEnumerable<RoleAccess>> GetAllAccesss()
        {
            return await _dynamicityRepository.GetAccessesAsync();
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



        public async Task<int> UpsertDocumentSetting(UpsertSetting input)
        {
            if (input.Id == null)
            {
                return await _dynamicityRepository.AddDocumentSettingAsync(new DocumentSettings
                {
                    Area = input.Area,
                    DocumentConfigsId = input.ConfigId,
                    MenuId = input.MenuId,
                });
            }
            else
            {
                var documentSetting = await _dynamicityRepository.GetDocumentSettingAsync((int)input.Id);
                if (documentSetting == null)
                {
                    throw new NotFoundException("تنظیمات یافت نشد");
                }
                documentSetting.Area = input.Area;
                documentSetting.DocumentConfigsId = input.ConfigId;
                documentSetting.MenuId = input.MenuId;

                return await _dynamicityRepository.UpdateDocumentSettingAsync(documentSetting);
            }
        }

        public async Task<int> DeleteDocumentSetting(int documentSettingId)
        {
            var documentSetting = await _dynamicityRepository.GetDocumentSettingAsync(documentSettingId);
            if (documentSetting == null)
            {
                throw new NotFoundException("تنظیمات یافت نشد");
            }

            return await _dynamicityRepository.DeleteDocumentSettingAsync(documentSetting);
        }

        public async Task<DocumentSettings> GetDocumentSetting(int documentSettingId)
        {
            var documentSetting = await _dynamicityRepository.GetDocumentSettingAsync(documentSettingId);
            if (documentSetting == null)
            {
                throw new NotFoundException("تنظیمات یافت نشد");
            }

            return documentSetting;
        }

        public async Task<IEnumerable<DocumentSettings>> GetDocumentSettings()
        {
            return await _dynamicityRepository.GetDocumentSettingsAsync();
        }

        public async Task<IEnumerable<DocumentSettings>> GetDocumentSettingsByMenuId(int menuId)
        {
            if (await _dynamicityRepository.GetMenuAsync(menuId) == null)
            {
                throw new NotFoundException("منو یافت نشد");
            }
            return await _dynamicityRepository.GetDocumentSettingsByMenuIdAsync(menuId);
        }

        public async Task<IEnumerable<DocumentSettings>> GetDocumentSettingsByArea(Area area)
        {
            return await _dynamicityRepository.GetDocumentSettingsByAreaAsync(area);
        }

        public async Task<IEnumerable<DocumentSettings>> GetDocumentSettingsByConfigId(int documentConfigId)
        {
            if (await _dynamicityRepository.GetDocumentConfigAsync(documentConfigId) == null)
            {
                throw new NotFoundException("کانفیگ یافت نشد");
            }

            return await _dynamicityRepository.GetDocumentSettingsByConfigIdAsync(documentConfigId);
        }

        public async Task<string> UpsertRole(UpsertRole input)
        {
            var role = await _dynamicityRepository.GetRoleByIdAsync(input.Id);
            if (role == null)
            {
                var result = await _dynamicityRepository.AddRoleAsync(input.Name);
                if (result.Succeeded)
                {
                    return input.Name;
                }
                return result.Errors.ToString();
            }
            else
            {
                role.Name = input.Name;
                var result = await _dynamicityRepository.UpdateRoleAsync(role);
                if (result.Succeeded)
                {
                    return input.Name;
                }
                throw new BadRequestException("مشکلی در به روز رسانی رول به وجود آمده است");
            }
        }

        public async Task<string> DeleteRole(string roleId)
        {
            var role = await _dynamicityRepository.GetRoleByIdAsync(roleId);
            if (role == null)
            {
                throw new NotFoundException("رول یافت نشد");
            }

            var result = await _dynamicityRepository.DeleteRoleAsync(role);
            if (result.Succeeded)
            {
                return role.Name;
            }

            throw new BadRequestException("مشکلی در حذف رول رخ داده است");
        }

        public async Task<RoleDTO> GetRole(string roleId)
        {
            var role = await _dynamicityRepository.GetRoleByIdAsync(roleId);
            if (role == null)
            {
                throw new NotFoundException("رول یافت نشد");
            }

            return new RoleDTO { Id = role.Id , Name = role.Name};
        }

        public async Task<IEnumerable<RoleDTO>> GetRoles()
        {
            var roles = await _dynamicityRepository.GetRolesAsync();
            return roles.Select(r => new RoleDTO
            {
                Id = r.Id,
                Name = r.Name
            });
        }

        public async Task<IEnumerable<RoleDTO>> GiveRoles(string userId, IEnumerable<string> roleNames)
        {
            var user = await _authenticationRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException("کاربر یافت نشد");
            }

            foreach (var roleName in roleNames)
            {
                if (await _dynamicityRepository.GetRoleByNameAsync(roleName) == null)
                {
                    throw new NotFoundException($"رول  '{roleName}' یافت نشد");
                }
            }

            var roles = await _dynamicityRepository.GiveRolesAsync(user, roleNames);

            return await Task.WhenAll(
                roles.Select(async roleName =>
                {
                    var role = await _dynamicityRepository.GetRoleByNameAsync(roleName);
                    return new RoleDTO
                    {
                        Id = role.Id,
                        Name = roleName
                    };
                })
            );

        }

        public async Task<IEnumerable<RoleDTO>> TakeRoles(string userId, IEnumerable<string> roleNames)
        {
            var user = await _authenticationRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException("کاربر یافت نشد");
            }

            foreach (var roleName in roleNames)
            {
                if (await _dynamicityRepository.GetRoleByNameAsync(roleName) == null)
                {
                    throw new NotFoundException($"رول  '{roleName}' یافت نشد");
                }
            }

            var roles = await _dynamicityRepository.TakeRolesAsync(user, roleNames);

            return await Task.WhenAll(
                roles.Select(async roleName =>
                {
                    var role = await _dynamicityRepository.GetRoleByNameAsync(roleName);
                    return new RoleDTO
                    {
                        Id = role.Id,
                        Name = roleName
                    };
                })
            );
        }
    }
}
