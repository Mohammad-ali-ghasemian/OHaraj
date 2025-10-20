using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OHaraj.Core.Domain.Entities.Configs;
using OHaraj.Core.Domain.Entities.Management;
using OHaraj.Core.Domain.Models.Dynamicity;
using OHaraj.Core.Domain.Models.Dynamicity.Configs;
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
        public async Task<IActionResult> AddAccess(UpsertRoleAccess input)
        {
            return new Response<int>(await _dynamicityService.UpsertAccess(input)).ToJsonResult();
        }

        [HttpPost("Update-Access")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> UpdateAccess(UpsertRoleAccess input)
        {
            return new Response<int>(await _dynamicityService.UpsertAccess(input)).ToJsonResult();
        }

        [HttpPost("Delete-Access")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> DeleteAccess(int accessId)
        {
            return new Response<int>(await _dynamicityService.DeleteAccess(accessId)).ToJsonResult();
        }

        [HttpGet("Get-Access-By-Id")]
        [Produces(typeof(Response<RoleAccess>))]
        public async Task<IActionResult> GetAccessById(int id)
        {
            return new Response<RoleAccess>(await _dynamicityService.GetAccess(id)).ToJsonResult();
        }

        /// <summary>
        /// What menus can the role access (beside anonymous menus)
        /// SuperAdmin/Admin and the user who has this role can use this
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("Get-Role-Accesses")]
        [Produces(typeof(Response<IEnumerable<RoleAccess>>))]
        public async Task<IActionResult> GetRoleAccesses(string roleId)
        {
            return new Response<IEnumerable<RoleAccess>>(await _dynamicityService.GetAccess(roleId)).ToJsonResult();
        }

        [HttpGet("Get-All-Accesses")]
        [Produces(typeof(Response<IEnumerable<RoleAccess>>))]
        public async Task<IActionResult> GetAllAccesses()
        {
            return new Response<IEnumerable<RoleAccess>>(await _dynamicityService.GetAllAccesss()).ToJsonResult();
        }




        [HttpPost("Add-Image-Config")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> AddImageConfig(UpsertImageConfig input)
        {
            return new Response<int>(await _dynamicityService.UpsertImageConfig(input)).ToJsonResult();
        }

        [HttpPost("Update-Image-Config")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> UpdateImageConfig(UpsertImageConfig input)
        {
            return new Response<int>(await _dynamicityService.UpsertImageConfig(input)).ToJsonResult();
        }

        [HttpPost("Delete-Image-Config")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> DeleteImageConfig(int imageConfigId)
        {
            return new Response<int>(await _dynamicityService.DeleteImageConfig(imageConfigId)).ToJsonResult();
        }

        [HttpGet("Get-Image-Config")]
        [Produces(typeof(Response<ImageConfigs>))]
        public async Task<IActionResult> GetImageConfig(int imageConfigId)
        {
            return new Response<ImageConfigs>(await _dynamicityService.GetImageConfig(imageConfigId)).ToJsonResult();
        }

        [HttpGet("Get-Image-Configs")]
        [Produces(typeof(Response<IEnumerable<ImageConfigs>>))]
        public async Task<IActionResult> GetImageConfigs()
        {
            return new Response<IEnumerable<ImageConfigs>>(await _dynamicityService.GetImageConfigs()).ToJsonResult();
        }



        [HttpPost("Add-Video-Config")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> AddVideoConfig(UpsertVideoConfig input)
        {
            return new Response<int>(await _dynamicityService.UpsertVideoConfig(input)).ToJsonResult();
        }

        [HttpPost("Update-Video-Config")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> UpdateVideoConfig(UpsertVideoConfig input)
        {
            return new Response<int>(await _dynamicityService.UpsertVideoConfig(input)).ToJsonResult();
        }

        [HttpPost("Delete-Video-Config")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> DeleteVideoConfig(int videoConfigId)
        {
            return new Response<int>(await _dynamicityService.DeleteVideoConfig(videoConfigId)).ToJsonResult();
        }

        [HttpGet("Get-Video-Config")]
        [Produces(typeof(Response<VideoConfigs>))]
        public async Task<IActionResult> GetVideoConfig(int videoConfigId)
        {
            return new Response<VideoConfigs>(await _dynamicityService.GetVideoConfig(videoConfigId)).ToJsonResult();
        }

        [HttpGet("Get-Video-Configs")]
        [Produces(typeof(Response<IEnumerable<VideoConfigs>>))]
        public async Task<IActionResult> GetVideoConfigs()
        {
            return new Response<IEnumerable<VideoConfigs>>(await _dynamicityService.GetVideoConfigs()).ToJsonResult();
        }



    }
}
