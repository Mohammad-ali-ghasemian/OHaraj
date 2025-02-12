using Microsoft.AspNetCore.Identity;
using OHaraj.Core.Domain.Models.Admin;
using OHaraj.Core.Domain.Models.Authentication;
using OHaraj.Core.Interfaces.Repositories;
using OHaraj.Infrastructure;
using System.Security.Claims;

namespace OHaraj.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _dbContext;
        public AdminRepository(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ApplicationDbContext dbContext
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        public async Task<SignInResult> SignInAsync(Login input)
        {
            return await _signInManager.PasswordSignInAsync(input.Username, input.Password, input.RememberMe, false);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> AddAdmin(IdentityUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> AddUserRolesAsync(IdentityUser user, IEnumerable<string> roles)
        {
            return await _userManager.AddToRolesAsync(user, roles);
        }

        public Task<IEnumerable<string>> GetUserRolesAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> GetUserByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> GetUserByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> GetUserByPrincipalAsync(ClaimsPrincipal principal)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IdentityUser>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IdentityUser>> GetUsersByRoleAsync(string roleName)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateUserAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteUserAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }
    }
}
