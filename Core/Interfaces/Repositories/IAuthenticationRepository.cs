using Microsoft.AspNetCore.Identity;
using OHaraj.Core.Domain.Entities.Management;
using OHaraj.Core.Domain.Models.Authentication;
using System.Security.Claims;

namespace OHaraj.Core.Interfaces.Repositories
{
    public interface IAuthenticationRepository
    {
        // User - Register
        Task<IdentityResult> AddUserAsync(IdentityUser user, string password);
        Task<IdentityResult> AddUserRolesAsync(IdentityUser user, IEnumerable<string> roles);
        Task<IEnumerable<string>> GetUserRolesAsync(IdentityUser user);

        Task<IdentityUser> GetUserByIdAsync(string userId);
        Task<IdentityUser> GetUserByUsernameAsync(string username);
        Task<IdentityUser> GetUserByEmailAsync(string email);
        Task<IEnumerable<IdentityUser>> GetAllUsersAsync();
        Task<IEnumerable<IdentityUser>> GetUsersByRoleAsync(string roleName);
        
        Task<IdentityResult> UpdateUserAsync(IdentityUser user);
        
        Task<IdentityResult> DeleteUserAsync(IdentityUser user);
        
        // Token
        Task AddUserTokensAsync(Token token);
        Task<Token> GetUserTokensAsync(string userId);
        Task<Token> GetTokensByEmailVerificationTokenAsync(string token);
        Task<Token> GetTokensByResetPasswordTokenAsync(string token);
        Task UpdateUserTokensAsync(Token token);

        // Login - Logout
        Task<SignInResult> SignInAsync(Login login);
        Task SignOutAsync();

        // Reset Password without old password
        Task<IdentityResult> RemoveUserPasswordAsync(IdentityUser user);
        Task<IdentityResult> AddUserPasswordAsync(IdentityUser user, string newPassword);
        // Change Password with old password
        Task<IdentityResult> ChangeUserPasswordAsync(IdentityUser user, ChangePassword input);
    }
}
