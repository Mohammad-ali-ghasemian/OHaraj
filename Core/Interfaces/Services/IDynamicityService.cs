using Microsoft.AspNetCore.Identity;
using OHaraj.Core.Domain.DTOs;
using OHaraj.Core.Domain.Entities.Configs;
using OHaraj.Core.Domain.Entities.Management;
using OHaraj.Core.Domain.Entities.Settings;
using OHaraj.Core.Domain.Models.Dynamicity;
using OHaraj.Core.Domain.Models.Dynamicity.Configs;
using OHaraj.Core.Enums;
using System.Data;
using System.Security.Principal;

namespace OHaraj.Core.Interfaces.Services
{
    public interface IDynamicityService
    {
        //By default, everyone can access all menus except the admins set specific roles for that menu
        //Menu
        Task<int> UpsertMenu(UpsertMenu input);
        Task<int> DeleteMenu(int menuId);
        Task<Menu> GetMenu(int menuId);
        Task<IEnumerable<Menu>> GetMenus();
        Task<bool> HasCurrentUserAccess(int menuId);
        Task<IEnumerable<Menu>> GetAnonymousUserAccessMenus();
        Task<IEnumerable<Menu>> GetLoginedUserAccessMenus();
        Task<IEnumerable<Menu>> GetOtherUserAccessMenus(string userId);



        //Role Access 
        Task<int> UpsertAccess(UpsertRoleAccess input);
        Task<int> DeleteAccess(int accessId);
        Task<RoleAccess> GetAccess(int accessId);
        Task<IEnumerable<RoleAccess>> GetAccess(string roleId);
        Task<IEnumerable<RoleAccess>> GetAllAccesss();



        //Configs
        Task<int> UpsertImageConfig(UpsertImageConfig input);
        Task<int> DeleteImageConfig(int imageConfigId);
        Task<ImageConfigs> GetImageConfig(int imageConfigId);
        Task<IEnumerable<ImageConfigs>> GetImageConfigs();

        Task<int> UpsertVideoConfig(UpsertVideoConfig input);
        Task<int> DeleteVideoConfig(int videoConfigId);
        Task<VideoConfigs> GetVideoConfig(int videoConfigId);
        Task<IEnumerable<VideoConfigs>> GetVideoConfigs();

        Task<int> UpsertAudioConfig(UpsertAudioConfig input);
        Task<int> DeleteAudioConfig(int audioConfigId);
        Task<AudioConfigs> GetAudioConfig(int audioConfigId);
        Task<IEnumerable<AudioConfigs>> GetAudioConfigs();

        Task<int> UpsertDocumentConfig(UpsertDocumentConfig input);
        Task<int> DeleteDocumentConfig(int documentConfigId);
        Task<DocumentConfigs> GetDocumentConfig(int documentConfigId);
        Task<IEnumerable<DocumentConfigs>> GetDocumentConfigs();



        //Settings
        Task<int> UpsertImageSetting(UpsertSetting input);
        Task<int> DeleteImageSetting(int imageSettingId);
        Task<ImageSettings> GetImageSetting(int imageSettingId);
        Task<IEnumerable<ImageSettings>> GetImageSettings();
        Task<IEnumerable<ImageSettings>> GetImageSettingsByMenuId(int menuId);
        Task<IEnumerable<ImageSettings>> GetImageSettingsByArea(Area area);
        Task<IEnumerable<ImageSettings>> GetImageSettingsByConfigId(int imageConfigId);

        Task<int> UpsertVideoSetting(UpsertSetting input);
        Task<int> DeleteVideoSetting(int videoSettingId);
        Task<VideoSettings> GetVideoSetting(int videoSettingId);
        Task<IEnumerable<VideoSettings>> GetVideoSettings();
        Task<IEnumerable<VideoSettings>> GetVideoSettingsByMenuId(int menuId);
        Task<IEnumerable<VideoSettings>> GetVideoSettingsByArea(Area area);
        Task<IEnumerable<VideoSettings>> GetVideoSettingsByConfigId(int videoConfigId);

        Task<int> UpsertAudioSetting(UpsertSetting input);
        Task<int> DeleteAudioSetting(int audioSettingId);
        Task<AudioSettings> GetAudioSetting(int audioSettingId);
        Task<IEnumerable<AudioSettings>> GetAudioSettings();
        Task<IEnumerable<AudioSettings>> GetAudioSettingsByMenuId(int menuId);
        Task<IEnumerable<AudioSettings>> GetAudioSettingsByArea(Area area);
        Task<IEnumerable<AudioSettings>> GetAudioSettingsByConfigId(int audioConfigId);

        Task<int> UpsertDocumentSetting(UpsertSetting input);
        Task<int> DeleteDocumentSetting(int documentSettingId);
        Task<DocumentSettings> GetDocumentSetting(int documentSettingId);
        Task<IEnumerable<DocumentSettings>> GetDocumentSettings();
        Task<IEnumerable<DocumentSettings>> GetDocumentSettingsByMenuId(int menuId);
        Task<IEnumerable<DocumentSettings>> GetDocumentSettingsByArea(Area area);
        Task<IEnumerable<DocumentSettings>> GetDocumentSettingsByConfigId(int documentConfigId);



        //Roles
        Task<string> UpsertRole(UpsertRole input);
        Task<string> DeleteRole(string roleId);
        Task<RoleDTO> GetRole(string roleId);
        Task<IEnumerable<RoleDTO>> GetRoles();

        // Default : everyone has "User" role
        // Default : cannot give "User" and "Admin" or "SuperAdmin" role to another user (SuperAdmin can give "admin")
        // Default : cannot take "User" and "Admin" or "SuperAdmin" role from another user
        // Give role to the user, gives back some identity roles for later : fetch the name of the roles (string) then return
        Task<IEnumerable<RoleDTO>> GiveRoles(string userId, IEnumerable<string> roleIds);
        Task<IEnumerable<RoleDTO>> TakeRoles(string userId, IEnumerable<string> roleIds);
    }
}
