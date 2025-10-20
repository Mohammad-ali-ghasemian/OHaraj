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


    }
}
