using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OHaraj.Core.Domain.DTOs;
using OHaraj.Core.Domain.Models.Authentication;
using OHaraj.Core.Interfaces.Services;
using Project.Application.Responses;
using System.Net;

namespace OHaraj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(typeof(Response<string>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(Response<string>), (int)HttpStatusCode.BadRequest)]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Register")]
        [Produces(typeof(Response<ResponseStatus>))]
        public async Task<IActionResult> Register(Register input)
        {
            return new Response<ResponseStatus>(await _authenticationService.Register(input)).ToJsonResult();
        }

        [HttpPost("Send-Verification-Email")]
        [Produces(typeof(Response<ResponseStatus>))]
        public async Task<IActionResult> SendVerificationEmail(string email)
        {
            return new Response<ResponseStatus>(await _authenticationService.SendVerificationEmail(email)).ToJsonResult();
        }

        [HttpPost("Verify-Email-Token")]
        [Produces(typeof(Response<string>))]
        public async Task<IActionResult> VerifyEmailToken(string token)
        {
            return new Response<string>(await _authenticationService.VerifiyEmailToken(token)).ToJsonResult();
        }

        [HttpPost("Send-Reset-Password-Email")]
        [Produces(typeof(Response<ResponseStatus>))]
        public async Task<IActionResult> SendResetPasswordEmail(string email)
        {
            return new Response<ResponseStatus>(await _authenticationService.SendResetPasswordEmail(email)).ToJsonResult();
        }

        [HttpPost("Verify-Reset-Token")]
        [Produces(typeof(Response<string>))]
        public async Task<IActionResult> VerifyResetToken(ResetPassword input)
        {
            return new Response<string>(await _authenticationService.VerifiyResetPasswordToken(input)).ToJsonResult();
        }

        [HttpPost("Change-Password")]
        [Produces(typeof(Response<UserDTO>))]
        public async Task<IActionResult> ChangePassword(ChangePassword input)
        {
            return new Response<UserDTO>(await _authenticationService.ChangePassword(input)).ToJsonResult();
        }

        [HttpPost("Login")]
        [Produces(typeof(Response<UserDTO>))]
        public async Task<IActionResult> Login(Login input)
        {
            return new Response<UserDTO>(await _authenticationService.Login(input)).ToJsonResult();
        }

        [HttpPost("Logout")]
        [Produces(typeof(Response<ResponseStatus>))]
        public async Task<IActionResult> Logout()
        {
            return new Response<ResponseStatus>(await _authenticationService.Logout()).ToJsonResult();
        }

        [Authorize]
        [HttpGet("Current-User")]
        [Produces(typeof(Response<UserDTO>))]
        public async Task<IActionResult> CurrentUser()
        {
            return new Response<UserDTO>(await _authenticationService.CurrentUser()).ToJsonResult();
        }

    }
}
