using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OHaraj.Core.Domain.Entities.Management;
using OHaraj.Core.Domain.Models.Dynamicity;
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
    public class DynamicityController : ControllerBase
    {
        private readonly IDynamicityService _dynamicityService;
        public DynamicityController(IDynamicityService dynamicityService)
        {
            _dynamicityService = dynamicityService;
        }

        [HttpPost("Add-Menu")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> AddMenu(UpsertMenu input) 
        {
            return new Response<int>(await _dynamicityService.UpsertMenu(input)).ToJsonResult();
        }

        [HttpPost("Update-Menu")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> UpdateMenu(UpsertMenu input)
        {
            return new Response<int>(await _dynamicityService.UpsertMenu(input)).ToJsonResult();
        }

        [HttpPost("Delete-Menu")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> DeleteMenu(int menuId)
        {
            return new Response<int>(await _dynamicityService.DeleteMenu(menuId)).ToJsonResult();
        }

        [HttpGet("Get-Menu")]
        [Produces(typeof(Response<Menu>))]
        public async Task<IActionResult> GetMenu(int menuId)
        {
            return new Response<Menu>(await _dynamicityService.GetMenu(menuId)).ToJsonResult();
        }

        /// <summary>
        /// Get all menus
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get-Menus")]
        [Produces(typeof(Response<IEnumerable<Menu>>))]
        public async Task<IActionResult> GetMenus()
        {
            return new Response<IEnumerable<Menu>>(await _dynamicityService.GetMenus()).ToJsonResult();
        }

        /// <summary>
        /// Send a menu id and see if the current user has access? (true/execption)
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Has-Current-User-Access")]
        [Produces(typeof(Response<bool>))]
        public async Task<IActionResult> HasCurrentUserAccess(int menuId)
        {
            return new Response<bool>(await _dynamicityService.HasCurrentUserAccess(menuId)).ToJsonResult();
        }

        /// <summary>
        /// By default, everyone can access all menus, except admins set specific roles for that menu
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Get-Anonymous-User-Access-Menus")]
        [Produces(typeof(Response<IEnumerable<Menu>>))]
        public async Task<IActionResult> GetAnonymousUserAccessMenus()
        {
            return new Response<IEnumerable<Menu>>(await _dynamicityService.GetAnonymousUserAccessMenus()).ToJsonResult();
        }

        /// <summary>
        /// Must login first to use this
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("Get-Logined-User-Access-Menus")]
        [Produces(typeof(Response<IEnumerable<Menu>>))]
        public async Task<IActionResult> GetLoginedUserAccessMenus()
        {
            return new Response<IEnumerable<Menu>>(await _dynamicityService.GetLoginedUserAccessMenus()).ToJsonResult();
        }

        /// <summary>
        /// Just for admins
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get-Other-User-Access-Menus")]
        [Produces(typeof(Response<IEnumerable<Menu>>))]
        public async Task<IActionResult> GetOtherUserAccessMenus(string userId)
        {
            return new Response<IEnumerable<Menu>>(await _dynamicityService.GetOtherUserAccessMenus(userId)).ToJsonResult();
        }




        /// <summary>
        /// Which role can access which menu
        /// </summary>
        /// <returns></returns>
        [HttpPost("Add-Access")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult]> AddAccess(UpsertRoleAccess input)
        {
            return new Response<int>(await _dynamicityService.UpsertAccess(input)).ToJsonResult();
        }

    }
}
