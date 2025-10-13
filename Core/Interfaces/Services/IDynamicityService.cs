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

    }
}
