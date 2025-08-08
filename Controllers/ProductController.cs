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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize("SuperAdmin,Admin")]
        [HttpPost("Add-Product")]
        [Produces(typeof(Response<ProductDTO>))]
        public async Task<IActionResult> AddProduct(UpsertProduct input)
        {
            return new Response<ProductDTO>(await _productService.AddProduct(input)).ToJsonResult();
        }

        [Authorize("SuperAdmin,Admin")]
        [HttpPost("Update-Product")]
        [Produces(typeof(Response<ProductDTO>))]
        public async Task<IActionResult> UpdateProduct(UpdateProduct input)
        {
            return new Response<ProductDTO>(await _productService.UpdateProduct(input)).ToJsonResult();
        }

        [HttpGet("Get-Product")]
        [Produces(typeof(Response<ProductDTO>))]
        public async Task<IActionResult> GetProduct(int productId)
        {
            return new Response<ProductDTO>(await _productService.GetProduct(productId)).ToJsonResult();
        }

        [HttpGet("Get-All-Products")]
        [Produces(typeof(Response<IEnumerable<ProductDTO>>))]
        public async Task<IActionResult> GetAllProducts()
        {
            return new Response<IEnumerable<ProductDTO>>(await _productService.GetAllProducts()).ToJsonResult();
        }

        [HttpGet("Get-Products-By-Category")]
        [Produces(typeof(Response<IEnumerable<ProductDTO>>))]
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            return new Response<IEnumerable<ProductDTO>>(await _productService.GetProductsByCategory(categoryId)).ToJsonResult();
        }

        [HttpGet("Get-Products-By-Model")]
        [Produces(typeof(Response<IEnumerable<ProductDTO>>))]
        public async Task<IActionResult> GetProductsByModel(int modelId)
        {
            return new Response<IEnumerable<ProductDTO>>(await _productService.GetProductsByModel(modelId)).ToJsonResult();
        }

        [Authorize("SuperAdmin,Admin")]
        [HttpPost("Delete-Product")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            return new Response<int>(await _productService.DeleteProduct(productId)).ToJsonResult();
        }

        [HttpGet("Is-Liked")]
        [Produces(typeof(Response<bool>))]
        public async Task<IActionResult> IsLiked(int productId)
        {
            return new Response<bool>(await _productService.IsLikedByUser(productId)).ToJsonResult();
        }

        [HttpPost("Add-Comment")]
        [Produces(typeof(Response<CommentDTO>))]
        public async Task<IActionResult> AddComment(UpsertComment input)
        {
            return new Response<CommentDTO>(await _productService.AddComment(input)).ToJsonResult();
        }

        [HttpPost("Update-Comment")]
        [Produces(typeof(Response<CommentDTO>))]
        public async Task<IActionResult> UpdateComment(UpsertComment input)
        {
            return new Response<CommentDTO>(await _productService.UpdateComment(input)).ToJsonResult();
        }

        [Authorize("SuperAdmin,Admin")]
        [HttpPost("Toggle-Approval-Comment")]
        [Produces(typeof(Response<CommentDTO>))]
        public async Task<IActionResult> ToggleApprovalComment(int commentId, bool status)
        {
            return new Response<CommentDTO>(await _productService.ToggleApprovalComment(commentId, status)).ToJsonResult();
        }
    }
}
