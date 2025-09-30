using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OHaraj.Core.Domain.DTOs;
using OHaraj.Core.Domain.Models.Product;
using OHaraj.Core.Interfaces.Services;
using Project.Application.Responses;
using System.Net;

namespace OHaraj.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(typeof(Response<string>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(Response<string>), (int)HttpStatusCode.BadRequest)]
    public class ModelController : ControllerBase
    {
        private readonly IModelService _modelService;
        public ModelController(IModelService modelService)
        {
            _modelService = modelService;
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost("Add-Model")]
        [Produces(typeof(Response<ModelDTO>))]
        public async Task<IActionResult> AddModel([FromForm] UpsertModel input)
        {
            return new Response<ModelDTO>(await _modelService.AddModel(input)).ToJsonResult();
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost("Update-Model")]
        [Produces(typeof(Response<ModelDTO>))]
        public async Task<IActionResult> UpdateModel([FromForm] UpsertModel input)
        {
            return new Response<ModelDTO>(await _modelService.UpdateModel(input)).ToJsonResult();
        }

        [HttpGet("Get-Model")]
        [Produces(typeof(Response<ModelDTO>))]
        public async Task<IActionResult> GetModel(int modelId)
        {
            return new Response<ModelDTO>(await _modelService.GetModel(modelId)).ToJsonResult();
        }

        [HttpGet("Get-All-Models")]
        [Produces(typeof(Response<IEnumerable<ModelDTO>>))]
        public async Task<IActionResult> GetAllModels()
        {
            return new Response<IEnumerable<ModelDTO>>(await _modelService.GetAllModels()).ToJsonResult();
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost("Delete-Model")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> DeleteModel(int modelId)
        {
            return new Response<int>(await _modelService.DeleteModel(modelId)).ToJsonResult();
        }


    }
}
