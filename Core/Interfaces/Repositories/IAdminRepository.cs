using Microsoft.AspNetCore.Identity;
using OHaraj.Core.Domain.Models.Authentication;

namespace OHaraj.Core.Interfaces.Repositories
{
    public interface IAdminRepository
    {
        Task<SignInResult> SignInAsync(Login login);
        Task SignOutAsync();
    }
}
