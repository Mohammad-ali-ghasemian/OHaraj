using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IdentityResult> AddAdmin(IdentityUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> AddRolesAsync(IdentityUser user, IEnumerable<string> roles)
        {
            return await _userManager.AddToRolesAsync(user, roles);
        }

        public async Task<IdentityResult> DemotionAdmin(IdentityUser user, string role)
        {
            return await _userManager.RemoveFromRoleAsync(user, role);
        }
    }
}
