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
        public async Task<IActionResult> AddProduct(Product input)
        {
            return new Response<ProductDTO>(await _productService.AddProduct(input)).ToJsonResult();
        }


    }
}
