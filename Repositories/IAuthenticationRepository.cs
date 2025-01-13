using Microsoft.AspNetCore.Identity;

namespace OHaraj.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<IdentityResult> AddUserAsync(IdentityUser user, string password);
        Task<IdentityUser> GetUserAsync(int userId);
        Task<IEnumerable<IdentityUser>> GetAllUsersAsync();
        Task<IEnumerable<IdentityUser>> GetUsersByRoleAsync(int roleId);
        Task<IEnumerable<IdentityUser>> GetUsersByRoleAsync(string role);
    }
}
