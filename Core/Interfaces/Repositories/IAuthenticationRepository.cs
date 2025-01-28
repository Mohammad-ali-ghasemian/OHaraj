using Microsoft.AspNetCore.Identity;
using OHaraj.Core.Domain.Entities.Management;
using OHaraj.Core.Domain.Models.Authentication;

namespace OHaraj.Core.Interfaces.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<IdentityResult> AddUserAsync(IdentityUser user, string password);
        Task<IdentityResult> AddUserRolesAsync(IdentityUser user, IEnumerable<string> roles);
        Task<IdentityResult> DeleteUserAsync(IdentityUser user);
        Task<IdentityUser> GetUserByIdAsync(string userId);
        Task<IdentityUser> GetUserByUsernameAsync(string username);
        Task<IdentityUser> GetUserByEmailAsync(string email);
        Task<IEnumerable<IdentityUser>> GetAllUsersAsync();
        Task<IEnumerable<IdentityUser>> GetUsersByRoleAsync(string roleName);
        Task<SignInResult> SignInAsync(Login login);
        Task<Token> GetUserTokensAsync(string userId);
        Task UpdateUserTokensAsync(Token token);
    }
}
