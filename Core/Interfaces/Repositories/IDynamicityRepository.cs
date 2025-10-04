using OHaraj.Core.Domain.Entities.Configs;
using OHaraj.Core.Domain.Entities.Handling;

namespace OHaraj.Core.Interfaces.Repositories
{
    public interface IDynamicityRepository
    {
        //Menu
        Task<int> AddMenuAsync(Menu input);
        Task<int> UpdateMenuAsync(Menu input);
        Task<int> DeleteMenuAsync(Menu input);

        Task<Menu> GetMenuAsync(int id);
        Task<IEnumerable<Menu>> GetMenusAsync();
        // first get all menus then remove these access denied menus and send
        Task<IEnumerable<Menu>> GetAccessDeniedMenusAsync(IEnumerable<string> roleIds);



        //Role Access Banned
        Task<int> AddBanAccessAsync(RoleAccessBanned input);
        Task<int> UpdateBanAccessAsync(RoleAccessBanned input);
        Task<int> DeleteBanAccessAsync(RoleAccessBanned input);

        Task<RoleAccessBanned> GetBanAccessAsync(int id);
        Task<IEnumerable<RoleAccessBanned>> GetBanAccessesAsync();
        Task<IEnumerable<RoleAccessBanned>> GetBanAccessByRoleAsync(int roleId);



    }
}
