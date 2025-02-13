using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OHaraj.Core.Domain.DTOs;
using OHaraj.Core.Domain.Models.Admin;
using OHaraj.Core.Domain.Models.Authentication;
using OHaraj.Core.Interfaces.Repositories;
using OHaraj.Core.Interfaces.Services;
using Project.Application.Exceptions;
using Project.Application.Responses;

namespace OHaraj.Services
{
    public class AdminService : IAdminService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAdminRepository _adminRepository;
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AdminService(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IAdminRepository adminRepository,
            IAuthenticationRepository authenticationRepository,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager
            )
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _adminRepository = adminRepository;
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

        public async Task<AdminDTO> AddAdmin(AdminRegister input)
        {
            if (await _authenticationRepository.GetUserByUsernameAsync(input.Username) != null)
            {
                throw new BadRequestException("نام کاربری وجود دارد");
            }
            if (input.Email != null && await _authenticationRepository.GetUserByEmailAsync(input.Email) != null)
            {
                throw new BadRequestException("ایمیل وجود دارد");
            }
            if (await _authenticationRepository.GetUserByPhoneNumberAsync(input.MobileNumber) != null)
            {
                throw new BadRequestException("شماره تلفن وجود دارد");
            }
            if (input.Password != input.ConfirmPassword)
            {
                throw new BadRequestException("تکرار رمز عبور صحیح نمی باشد");
            }

            var user = new IdentityUser
            {
                UserName = input.Username,
                Email = input.Email.ToLower(),
                EmailConfirmed = true,
                PhoneNumber = input.MobileNumber,
                PhoneNumberConfirmed = true
            };

            IdentityResult result = await _authenticationRepository.AddUserAsync(user, input.Password);

            if (result.Succeeded)
            {
                var addRoleResult = await _authenticationRepository.AddUserRolesAsync(user, new List<string> { "User", "Admin" });

                if (addRoleResult.Succeeded)
                {
                    // if add user and add role both worked correct
                    var adminDto = _mapper.Map<AdminDTO>(user);
                    adminDto.Roles = await _authenticationRepository.GetUserRolesAsync(user);
                    return adminDto;
                }

                await _authenticationRepository.DeleteUserAsync(user);
                throw new BadRequestException("مشکلی در ایجاد حساب به وجود آمد");
            }
            else
            {
                throw new BadRequestException(string.Join("<br>", result.Errors.Select(e => e.Description).ToList()));
            }
        }

        public async Task<UserDTO> DemotionAdmin(string id)
        {
            var user = await _authenticationRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("کاربر یافت نشد");
            }

            var result = await _adminRepository.RemoveAdminRoleAsync(user, "Admin");
            if (result.Succeeded)
            {
                var userDto = _mapper.Map<UserDTO>(user);
                userDto.Roles = await _authenticationRepository.GetUserRolesAsync(user);
                return userDto;
            }
            else
            {
                throw new BadRequestException("مشکلی در تنزل ادمین پیش آمد");
            }
        }

        public async Task<IEnumerable<AdminDTO>> GetAdmins()
        {
            var admins = (await _authenticationRepository.GetUsersByRoleAsync("Admin")).ToList();
            var adminsDto = _mapper.Map<List<AdminDTO>>(admins);
            for(int i=0; i<admins.Count(); i++)
            {
                adminsDto[i].Roles = await _authenticationRepository.GetUserRolesAsync(admins[i]);
            }

            return adminsDto;
        }

        public async Task<AdminDTO> ChangePassword(ChangePassword input)
        {
            if (input.Password != input.ConfirmPassword)
            {
                throw new BadRequestException("تکرار رمز عبور صحیح نمی‌باشد");
            }

            var user = await Current();
            if (user == null)
            {
                throw new NotFoundException("کاربر یافت نشد");
            }

            var result = await _authenticationRepository.ChangeUserPasswordAsync(user, input);
            if (!result.Succeeded)
            {
                throw new BadRequestException("رمز قبلی را صحیح وارد کنید");
            }

            var adminDto = _mapper.Map<AdminDTO>(user);
            adminDto.Roles = await _authenticationRepository.GetUserRolesAsync(user);
            return adminDto;
        }

        public async Task<AdminDTO> CurrentAdmin()
        {
            var user = await Current();
            var adminDto = _mapper.Map<AdminDTO>(user);
            adminDto.Roles = await _authenticationRepository.GetUserRolesAsync(user);
            return adminDto;
        }
    }
}
