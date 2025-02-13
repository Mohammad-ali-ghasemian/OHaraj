using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OHaraj.Core.Domain.DTOs;
using OHaraj.Core.Domain.Models.Admin;
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

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("Add-Admin")]
        [Produces(typeof(Response<AdminDTO>))]
        public async Task<IActionResult> AddAdmin(AdminRegister input)
        {
            return new Response<AdminDTO>(await _adminService.AddAdmin(input)).ToJsonResult();
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost("Demotion-Admin")]
        [Produces(typeof(Response<UserDTO>))]
        public async Task<IActionResult> DemotionAdmin(string adminId)
        {
            return new Response<UserDTO>(await _adminService.DemotionAdmin(adminId)).ToJsonResult();
        }

        [HttpPost("Change-Password")]
        [Produces(typeof(Response<AdminDTO>))]
        public async Task<IActionResult> ChangePassword(ChangePassword input)
        {
            return new Response<AdminDTO>(await _adminService.ChangePassword(input)).ToJsonResult();
        }

    }
}
