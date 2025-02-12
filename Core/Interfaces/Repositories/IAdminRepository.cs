using Microsoft.AspNetCore.Identity;
using OHaraj.Core.Domain.Models.Authentication;
using System.Security.Claims;

namespace OHaraj.Core.Interfaces.Repositories
{
    public interface IAdminRepository
    {
        Task<IdentityResult> AddAdmin(IdentityUser user, string password);
        Task<IdentityResult> AddRolesAsync(IdentityUser user, IEnumerable<string> roles);s
    }
}
