using OHaraj.Core.Domain.Entities.Configs;
using OHaraj.Core.Domain.Entities.Management;
using OHaraj.Core.Domain.Models.Dynamicity;

namespace OHaraj.Core.Interfaces.Services
{
    public interface IDynamicityService
    {
        Task<int> UpsertMenu(UpsertMenu input);
        Task<int> DeleteMenu(int menuId);
        Task<Menu> GetMenu(int menuId);
        Task<IEnumerable<Menu>> GetMenus();
        Task<IEnumerable<Menu>> GetMyAccessMenus();
        Task<IEnumerable<Menu>> GetOtherAccessMenus(string userId);

        Task<int> UpsertAccessBan(UpsertRoleAccessBanned input);
        Task<int> DeleteAccessBan(int accessBanId);
        Task<RoleAccessBanned> GetAccessBan(int id);
        Task<IEnumerable<RoleAccessBanned>> GetAccessBan(string userId);
        Task<IEnumerable<RoleAccessBanned>> GetAllAccessBans();

        Task<IEnumerable<ImageConfigs>> UpsertImageConfig(UpsertImageConfig input);
        Task<int> DeleteImageConfig(int imageConfigId);
        Task<ImageConfigs> GetImageConfig(int imageConfigId);
        Task<IEnumerable<ImageConfigs>> GetImageConfigs();
    }
}
