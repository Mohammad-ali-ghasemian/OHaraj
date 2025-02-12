using Microsoft.AspNetCore.Identity;
using OHaraj.Core.Domain.Models.Authentication;
using System.Security.Claims;

namespace OHaraj.Core.Interfaces.Repositories
{
    public interface IAdminRepository
    {
        Task<SignInResult> SignInAsync(Login input);
        Task SignOutAsync();
        Task<IdentityResult> AddAdmin(IdentityUser user, string password);
        Task<IdentityResult> AddUserRolesAsync(IdentityUser user, IEnumerable<string> roles);
        Task<IEnumerable<string>> GetUserRolesAsync(IdentityUser user);

        Task<IdentityUser> GetUserByIdAsync(string userId);
        Task<IdentityUser> GetUserByUsernameAsync(string username);
        Task<IdentityUser> GetUserByEmailAsync(string email);
        Task<IdentityUser> GetUserByPrincipalAsync(ClaimsPrincipal principal);
        Task<IEnumerable<IdentityUser>> GetAllUsersAsync();
        Task<IEnumerable<IdentityUser>> GetUsersByRoleAsync(string roleName);

        Task<IdentityResult> UpdateUserAsync(IdentityUser user);

        Task<IdentityResult> DeleteUserAsync(IdentityUser user);
    }
}
