using Microsoft.AspNetCore.Identity;
using OHaraj.Core.Domain.Models.Authentication;

namespace OHaraj.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<IdentityResult> AddUserAsync(IdentityUser user, string password);
        Task<IdentityUser> GetUserAsync(string userId);
        Task<IEnumerable<IdentityUser>> GetAllUsersAsync();
        Task<IEnumerable<IdentityUser>> GetUsersByRoleAsync(string roleName);
        Task<SignInResult> SignInAsync(Login login);
    }
}
