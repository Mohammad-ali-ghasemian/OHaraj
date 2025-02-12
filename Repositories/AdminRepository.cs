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
            throw new NotImplementedException();
        }

        public async Task SignOutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
