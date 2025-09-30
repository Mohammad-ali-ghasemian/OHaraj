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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost("Add-Category")]
        [Produces(typeof(Response<CategoryDTO>))]
        public async Task<IActionResult> AddCategory([FromForm] UpsertCategory input)
        {
            return new Response<CategoryDTO>(await _categoryService.AddCategory(input)).ToJsonResult();
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost("Update-Category")]
        [Produces(typeof(Response<CategoryDTO>))]
        public async Task<IActionResult> UpdateCategory([FromForm] UpsertCategory input)
        {
            return new Response<CategoryDTO>(await _categoryService.UpdateCategory(input)).ToJsonResult();
        }

        [HttpGet("Get-Category")]
        [Produces(typeof(Response<CategoryDTO>))]
        public async Task<IActionResult> GetCategory(int categoryId)
        {
            return new Response<CategoryDTO>(await _categoryService.GetCategory(categoryId)).ToJsonResult();
        }

        [HttpGet("Get-All-Categorys")]
        [Produces(typeof(Response<IEnumerable<CategoryDTO>>))]
        public async Task<IActionResult> GetAllCategories()
        {
            return new Response<IEnumerable<CategoryDTO>>(await _categoryService.GetAllCategories()).ToJsonResult();
        }

        [Authorize(Roles = "SuperAdmin,Admin")]
        [HttpPost("Delete-Category")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            return new Response<int>(await _categoryService.DeleteCategory(categoryId)).ToJsonResult();
        }

        
    }
}
