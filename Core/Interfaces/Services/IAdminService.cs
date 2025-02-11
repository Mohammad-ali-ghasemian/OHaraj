using OHaraj.Core.Domain.DTOs;
using OHaraj.Core.Domain.Models.Authentication;

namespace OHaraj.Core.Interfaces.Services
{
    public interface IAdminService
    {
        Task<UserDTO> AddAdmin(Register input);
        Task<UserDTO> UpdateAdmin();
        Task<UserDTO> Login(Login input);
    }
}
