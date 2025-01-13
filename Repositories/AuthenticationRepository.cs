using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OHaraj.Core.Domain.Models.Authentication;
using System.Data;

namespace OHaraj.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AuthenticationRepository(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> AddUserAsync(IdentityUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<SignInResult> SignInAsync(Login login)
        {
            return await _signInManager.PasswordSignInAsync(login.Email, login.Password, login.rememberMe, false);
        }

        public async Task<IEnumerable<IdentityUser>> GetAllUsersAsync()
        {
            return await Task.FromResult(_userManager.Users.ToList());
        }

        public async Task<IdentityUser> GetUserAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<IEnumerable<IdentityUser>> GetUsersByRoleAsync(string roleName)
        {
            return await _userManager.Users.Where(user => _userManager.IsInRoleAsync(user, roleName).Result).ToListAsync();
        }
    }
}
