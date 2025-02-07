using Microsoft.AspNetCore.Identity;
using OHaraj.Core.Domain.DTOs;
using OHaraj.Core.Domain.Models.Authentication;
using Project.Application.Responses;

namespace OHaraj.Core.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<UserDTO> Login(Login input);
        Task<ResponseStatus> Register(Register input);
        Task<ResponseStatus> SendVerificationEmail(string email);
        Task<string> VerifiyEmailToken(string token);
        Task<ResponseStatus> SendResetPasswordEmail(string email);
        Task<string> VerifiyResetPasswordToken(ResetPassword input);
        Task<UserDTO> ChangePassword(ChangePassword input);
    }
}
