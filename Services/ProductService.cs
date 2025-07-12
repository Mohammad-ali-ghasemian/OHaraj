using AutoMapper;
using OHaraj.Core.Domain.DTOs;
using OHaraj.Core.Domain.Models.Product;
using OHaraj.Core.Interfaces.Repositories;
using OHaraj.Core.Interfaces.Services;

namespace OHaraj.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductService(
            IProductRepository productRepository,
            IAuthenticationRepository authenticationRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _productRepository = productRepository;
            _authenticationRepository = authenticationRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<ProductDTO> AddProduct(UpsertProduct input)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public Task<ProductDTO> GetProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDTO>> GetProductsByCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDTO> ToggleProductActivation(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDTO> UpdateProduct(UpsertProduct input)
        {
            throw new NotImplementedException();
        }
    }
}
