using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OHaraj.Core.Domain.DTOs;
using OHaraj.Core.Domain.Models.Authentication;
using OHaraj.Core.Interfaces.Repositories;
using OHaraj.Core.Interfaces.Services;
using Project.Application.Exceptions;
using Project.Application.Responses;
using System.Security.Cryptography;

namespace OHaraj.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthenticationRepository _authenticationRepository;
        public AuthenticationService(IMapper mapper, IHttpContextAccessor httpContextAccessor, IAuthenticationRepository authenticationRepository) {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _authenticationRepository = authenticationRepository;
        }

        public UserDTO Current()
        {
            return (UserDTO)_httpContextAccessor.HttpContext.Items["User"];
        }

        public static string CreateToken(int n)
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(n));
        }

        public async Task<ResponseStatus> Register(Register input)
        {
            if (_authenticationRepository.GetUserByUsernameAsync(input.Username) != null)
            {
                throw new BadRequestException("نام کاربری وجود دارد");
            }
            if (input.Email != null && _authenticationRepository.GetUserByEmailAsync(input.Email) != null)
            {
                throw new BadRequestException("ایمیل وجود دارد");
            }
            if (input.Password != input.ConfirmPassword)
            {
                throw new BadRequestException("تکرار رمز عبور صحیح نمی باشد");
            }

            var user = new IdentityUser
            {
                UserName = input.Username,
                Email = input.Email
            };

            IdentityResult result = await _authenticationRepository.AddUserAsync(user, input.Password);

            if (result.Succeeded)
            {
                var addRoleResult = await _authenticationRepository.AddUserRolesAsync(user, new List<string> {"User"});

                if (addRoleResult.Succeeded)
                {
                    // if add user and add role both worked correct
                    return ResponseStatus.Succeed;
                }

                await _authenticationRepository.DeleteUserAsync(user);
                throw new BadRequestException("مشکلی در ایجاد حساب به وجود آمد");
            }
            else
            {
                throw new BadRequestException("مشکلی در ایجاد حساب به وجود آمد");
            }
        }
    }
}
