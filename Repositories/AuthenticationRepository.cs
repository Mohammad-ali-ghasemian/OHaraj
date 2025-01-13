using Microsoft.AspNetCore.Identity;

namespace OHaraj.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        public AuthenticationRepository(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddUserAsync(IdentityUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public Task<IEnumerable<IdentityUser>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> GetUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IdentityUser>> GetUsersByRoleAsync(int roleId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<IdentityUser>> GetUsersByRoleAsync(string role)
        {
            throw new NotImplementedException();
        }
    }
}
