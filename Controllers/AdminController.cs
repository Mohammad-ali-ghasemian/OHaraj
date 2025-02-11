using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OHaraj.Core.Domain.DTOs;
using OHaraj.Core.Domain.Models.Authentication;
using OHaraj.Core.Interfaces.Services;
using Project.Application.Responses;
using System.Net;

namespace OHaraj.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(typeof(Response<string>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(Response<string>), (int)HttpStatusCode.BadRequest)]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [Authorize(Roles = ("SuperAdmin"))]
        [HttpPost("Login")]
        [Produces(typeof(Response<UserDTO>))]
        public async Task<IActionResult> Login(Login input)
        {
            return new Response<UserDTO>(await _adminService.Login()).ToJsonResult();
        }
    }
}
