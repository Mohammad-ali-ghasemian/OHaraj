using OHaraj.Core.Domain.Entities.Management;
using OHaraj.Core.Domain.Models.Dynamicity;

namespace OHaraj.Core.Interfaces.Services
{
    public interface IDynamicityService
    {
        Task<int> UpsertMenu(UpsertMenu input);
        Task<Menu> GetMenu(int menuId);
        Task<IEnumerable<Menu>> GetMenus();

    }
}
