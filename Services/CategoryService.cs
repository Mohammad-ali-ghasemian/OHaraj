using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OHaraj.Core.Domain.DTOs;
using OHaraj.Core.Domain.Models.Product;
using OHaraj.Core.Interfaces.Repositories;
using OHaraj.Core.Interfaces.Services;
using Project.Application.Contracts.Infrastructure;

namespace OHaraj.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string FileContainer;
        private readonly IFileStorageService _uploaderService;
        public CategoryService(
            ICategoryRepository categoryRepository,
            IAuthenticationRepository authenticationRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IFileStorageService uploaderService
            )
        {
            _categoryRepository = categoryRepository;
            _authenticationRepository = authenticationRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            FileContainer = "Product";
            _uploaderService = uploaderService;
        }

        public async Task<IdentityUser> Current()
        {
            var userPrincipal = _httpContextAccessor.HttpContext?.User;
            if (userPrincipal == null)
            {
                return null;
            }

            var user = await _authenticationRepository.GetUserByPrincipalAsync(userPrincipal);
            return user;
        }
        public Task<CategoryDTO> AddCategory(UpsertCategory input)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteCategory(int CategoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CategoryDTO>> GetAllCategories(string filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDTO> GetCategory(int CategoryId)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDTO> UpdateCategory(UpsertCategory input)
        {
            throw new NotImplementedException();
        }
    }
}
