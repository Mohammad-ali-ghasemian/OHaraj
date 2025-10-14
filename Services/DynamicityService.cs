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

        public async Task<IEnumerable<Menu>> GetLoginUserAccessMenus()
        {
            var user = await Current();
            if (user == null)
            {
                throw new NotFoundException("کاربر وارد نشده است.");
            }

            var accessBanned = await _dynamicityRepository.GetAccessDeniedMenusAsync(await _authenticationRepository.GetUserRolesAsync(user));
            var menus = await _dynamicityRepository.GetMenusAsync();

            return menus.Except(accessBanned);
        }

        public Task<int> DeleteImageConfig(int imageConfigId)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteImageSetting(int imageSettingId)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteMenu(int menuId)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteVideoConfig(int videoConfigId)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteVideoSetting(int videoSettingId)
        {
            throw new NotImplementedException();
        }

        public Task<RoleAccessBanned> GetAccessBan(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RoleAccessBanned>> GetAccessBan(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RoleAccessBanned>> GetAllAccessBans()
        {
            throw new NotImplementedException();
        }

        public Task<AudioConfigs> GetAudioConfig(int audioConfigId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AudioConfigs>> GetAudioConfigs()
        {
            throw new NotImplementedException();
        }

        public Task<AudioSettings> GetAudioSetting(int audioSettingId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AudioSettings>> GetAudioSettings()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AudioSettings>> GetAudioSettingsByArea(Area area)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AudioSettings>> GetAudioSettingsByConfigId(int audioConfigId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AudioSettings>> GetAudioSettingsByMenuId(int menuId)
        {
            throw new NotImplementedException();
        }

        public Task<DocumentConfigs> GetDocumentConfig(int documentConfigId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DocumentConfigs>> GetDocumentConfigs()
        {
            throw new NotImplementedException();
        }

        public Task<DocumentSettings> GetDocumentSetting(int documentSettingId)
        {
            throw new NotImplementedException();
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

        public Task<VideoConfigs> GetVideoConfig(int videoConfigId)
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
