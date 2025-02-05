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


    }
}
