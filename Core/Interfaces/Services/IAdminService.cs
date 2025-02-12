using OHaraj.Core.Domain.DTOs;
using OHaraj.Core.Domain.Models.Admin;
using OHaraj.Core.Domain.Models.Authentication;

namespace OHaraj.Core.Interfaces.Services
{
    public interface IAdminService
    {
        Task<AdminDTO> AddAdmin(AdminRegister input);
        Task<AdminDTO> UpdateAdmin();
        Task<UserDTO> DemotionAdmin();
    }
}
