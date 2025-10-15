using OHaraj.Core.Domain.Entities.Configs;
using OHaraj.Core.Domain.Entities.Management;
using OHaraj.Core.Domain.Entities.Settings;
using OHaraj.Core.Domain.Models.Dynamicity;
using OHaraj.Core.Domain.Models.Dynamicity.Configs;
using OHaraj.Core.Enums;

namespace OHaraj.Core.Interfaces.Services
{
    public interface IDynamicityService
    {
        //Menu
        Task<int> UpsertMenu(UpsertMenu input);
        Task<int> DeleteMenu(int menuId);
        Task<Menu> GetMenu(int menuId);
        Task<IEnumerable<Menu>> GetMenus();
        Task<IEnumerable<Menu>> GetLoginedUserAccessMenus();
        Task<IEnumerable<Menu>> GetOtherUserAccessMenus(string userId);



        //Role Access Banned
        Task<int> UpsertAccessBan(UpsertRoleAccessBanned input);
        Task<int> DeleteAccessBan(int accessBanId);
        Task<RoleAccessBanned> GetAccessBan(int accessBanId);
        Task<IEnumerable<RoleAccessBanned>> GetAccessBan(string roleId);
        Task<IEnumerable<RoleAccessBanned>> GetAllAccessBans();



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
    }
}
