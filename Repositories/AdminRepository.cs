using Microsoft.AspNetCore.Identity;
using OHaraj.Core.Domain.Models.Authentication;
using OHaraj.Core.Interfaces.Repositories;
using OHaraj.Infrastructure;

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
        public async Task<SignInResult> SignInAsync(Login login)
        {
            return await _signInManager.PasswordSignInAsync(login.Username, login.Password, login.RememberMe, false);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
