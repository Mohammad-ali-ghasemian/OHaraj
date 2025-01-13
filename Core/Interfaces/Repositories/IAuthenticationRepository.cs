using Microsoft.AspNetCore.Identity;
using OHaraj.Core.Domain.Entities.Management;
using OHaraj.Core.Domain.Models.Authentication;

namespace OHaraj.Core.Interfaces.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<IdentityResult> AddUserAsync(IdentityUser user, string password);
        Task<IdentityUser> GetUserAsync(string userId);
        Task<IEnumerable<IdentityUser>> GetAllUsersAsync();
        Task<IEnumerable<IdentityUser>> GetUsersByRoleAsync(string roleName);
        Task<Token> GetUserTokensAsync(string userId);
        Task<SignInResult> SignInAsync(Login login);
    }
}
