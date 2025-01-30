using Microsoft.AspNetCore.Identity;
using OHaraj.Core.Domain.Models.Authentication;
using Project.Application.Responses;

namespace OHaraj.Core.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<ResponseStatus> Register(Register input);
        Task<ResponseStatus> SendVerificationEmail(string email);// create token and add it to token table;
        Task<string> VerifiyEmailToken(string email);// email verification in User table = true; verifiadAt in Token table;
        Task<ResponseStatus> SendResetPasswordEmail(string email);// create token and add it to token table; create expire date;
        Task<string> VerifiyResetPasswordToken(string email);
        Task<ResponseStatus> Login(Login input);
    }
}
