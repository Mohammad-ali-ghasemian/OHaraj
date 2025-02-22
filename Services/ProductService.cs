using AutoMapper;
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
    }
}
