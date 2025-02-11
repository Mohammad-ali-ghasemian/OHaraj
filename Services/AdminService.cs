using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OHaraj.Core.Interfaces.Repositories;

namespace OHaraj.Services
{
    public class AdminService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AdminService(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IAuthenticationRepository authenticationRepository,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager
            )
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _authenticationRepository = authenticationRepository;
            _userManager = userManager;
            _signInManager = signInManager;
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
    }
}
