using OHaraj.Core.Domain.Entities.Configs;
using OHaraj.Core.Domain.Entities.Management;
using OHaraj.Core.Domain.Models.Dynamicity;

namespace OHaraj.Core.Interfaces.Services
{
    public interface IDynamicityService
    {
        //Menu
        Task<int> UpsertMenu(UpsertMenu input);
        Task<int> DeleteMenu(int menuId);
        Task<Menu> GetMenu(int menuId);
        Task<IEnumerable<Menu>> GetMenus();
        Task<IEnumerable<Menu>> GetMyAccessMenus();
        Task<IEnumerable<Menu>> GetOtherAccessMenus(string userId);



        //Role Access Banned
        Task<int> UpsertAccessBan(UpsertRoleAccessBanned input);
        Task<int> DeleteAccessBan(int accessBanId);
        Task<RoleAccessBanned> GetAccessBan(int id);
        Task<IEnumerable<RoleAccessBanned>> GetAccessBan(string userId);
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
    }
}
