using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OHaraj.Core.Domain.Entities.Management;
using OHaraj.Core.Domain.Models.Authentication;
using OHaraj.Core.Interfaces.Repositories;
using OHaraj.Infrastructure;
using System.Data;

namespace OHaraj.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _dbContext;
        public AuthenticationRepository(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ApplicationDbContext dbContext
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        public async Task<IdentityResult> AddUserAsync(IdentityUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> AddUserRolesAsync(IdentityUser user, IEnumerable<string> roles)
        {
            return await _userManager.AddToRolesAsync(user, roles);
        }

        public async Task<SignInResult> SignInAsync(Login login)
        {
            return await _signInManager.PasswordSignInAsync(login.Username, login.Password, login.rememberMe, false);
        }

        public async Task<IEnumerable<IdentityUser>> GetAllUsersAsync()
        {
            return await Task.FromResult(_userManager.Users.ToList());
        }

        public async Task<IdentityUser> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<IdentityUser> GetUserByUsernameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<IdentityUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);

        }

        public async Task<IdentityResult> UpdateUserAsync(IdentityUser user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> DeleteUserAsync(IdentityUser user)
        {
            return await _userManager.DeleteAsync(user);
        }

        public async Task<IEnumerable<IdentityUser>> GetUsersByRoleAsync(string roleName)
        {
            return await _userManager.Users.Where(user => _userManager.IsInRoleAsync(user, roleName).Result).ToListAsync();
        }

        public async Task AddUserTokensAsync(Token token)
        {
            await _dbContext.Tokens.AddAsync(token);
            await _dbContext.SaveChangesAsync();
        }

        
        public async Task UpdateUserTokensAsync(Token token)
        {
            _dbContext.Update(token);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IdentityResult> RemoveUserPasswordAsync(IdentityUser user)
        {
            return await _userManager.RemovePasswordAsync(user);
        }

        public async Task<IdentityResult> AddUserPasswordAsync(IdentityUser user, string newPassword)
        {
            return await _userManager.AddPasswordAsync(user, newPassword);
        }
        
    }
}
