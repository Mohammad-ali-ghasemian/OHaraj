using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OHaraj.Core.Domain.DTOs;
using OHaraj.Core.Domain.Models.Authentication;
using OHaraj.Core.Interfaces.Repositories;
using OHaraj.Core.Interfaces.Services;
using Project.Application.Exceptions;
using Project.Application.Responses;
using System.Security.Cryptography;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using OHaraj.Core.Domain.Entities.Management;
using Newtonsoft.Json.Linq;

namespace OHaraj.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly UserManager<IdentityUser> _userManager;
        public AuthenticationService(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IAuthenticationRepository authenticationRepository,
            UserManager<IdentityUser> userManager
            ) 
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _authenticationRepository = authenticationRepository;
            _userManager = userManager;
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
            if (await _authenticationRepository.GetUserByUsernameAsync(input.Username) != null)
            {
                throw new BadRequestException("نام کاربری وجود دارد");
            }
            if (input.Email != null && await _authenticationRepository.GetUserByEmailAsync(input.Email) != null)
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
                Email = input.Email.ToLower()
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

        //create and then send token
        public async Task<ResponseStatus> SendVerificationEmail(string Email)
        {
            var user = await _authenticationRepository.GetUserByEmailAsync(Email);
            if (user == null)
            {
                throw new NotFoundException("ایمیل یافت نشد");
            }

            string token = CreateToken(4);
            var userToken = await _authenticationRepository.GetUserTokensAsync(user.Id);
            if (userToken.EmailVerificationToken != null)
            {
                userToken.EmailVerificationToken = token;
                await _authenticationRepository.UpdateUserTokensAsync(userToken);
            }
            else
            {
                await _authenticationRepository.AddUserTokensAsync(new Core.Domain.Entities.Management.Token
                {
                    UserId = user.Id,
                    EmailVerificationToken = token
                });
            }
            
            //starting assign mail server
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("MoqasSupport@moqas-chat.ir"));
            email.To.Add(MailboxAddress.Parse(Email));
            email.Subject = "Do Not Reply";
            email.Body = new TextPart(TextFormat.Html) { Text = $"کد فعال سازی شما<br/><b>{token}</b>" };

            using var smtp = new SmtpClient();
            {
                try
                {
                    smtp.Connect("webmail.moqas-chat.ir", 587, false);
                    smtp.Authenticate("MoqasSupport@moqas-chat.ir", "fF#90a54c");
                    smtp.Send(email);
                    smtp.Disconnect(true);
                }
                catch (Exception ex) {
                    throw new BadRequestException("مشکلی در ارسال ایمیل رخ داده است");
                }
            }
            //end of assign

            return ResponseStatus.Succeed;
        }


        public async Task<string> VerifiyEmailToken(string token)
        {
            var Token = await _authenticationRepository.GetTokensByEmailVerificationTokenAsync(token);
            if (Token == null)
            {
                throw new NotFoundException("کد صحیح نمی باشد");
            }

            var user = await _authenticationRepository.GetUserByIdAsync(Token.UserId);
            if (user == null)
            {
                throw new NotFoundException("کاربر وجود ندارد");
            }

            Token.EmailVerifiedAt = DateTime.Now;
            await _authenticationRepository.UpdateUserTokensAsync(Token);
            user.EmailConfirmed = true;
            await _authenticationRepository.UpdateUserAsync(user);

            return user.Email;
        }

        // create and then send token
        public async Task<ResponseStatus> SendResetPasswordEmail(string Email)
        {
            var user = await _authenticationRepository.GetUserByEmailAsync(Email);
            if (user == null)
            {
                throw new NotFoundException("ایمیل یافت نشد");
            }

            var userTokens = await _authenticationRepository.GetUserTokensAsync(user.Id);
            string token = CreateToken(4);

            userTokens.ResetPasswordToken = token;
            userTokens.ResetPasswordTokenExpires = DateTime.Now.AddHours(1);

            await _authenticationRepository.UpdateUserTokensAsync(userTokens);

            //starting assign mail server
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("MoqasSupport@moqas-chat.ir"));
            email.To.Add(MailboxAddress.Parse(Email));
            email.Subject = "Do Not Reply";
            email.Body = new TextPart(TextFormat.Html) { Text = $"کد بازیابی رمز شما<br/><b>{token}</b>" };

            using var smtp = new SmtpClient();
            {
                try
                {
                    smtp.Connect("webmail.moqas-chat.ir", 587, false);
                    smtp.Authenticate("MoqasSupport@moqas-chat.ir", "fF#90a54c");
                    smtp.Send(email);
                    smtp.Disconnect(true);
                }
                catch (Exception ex)
                {
                    throw new BadRequestException("مشکلی در ارسال ایمیل رخ داده است");
                }
            }
            //end of assign

            return ResponseStatus.Succeed;
        }

        // verify token & new password
        public async Task<string> VerifiyResetPasswordToken(ResetPassword input)
        {
            if (input.Password != input.ConfirmPassword)
            {
                throw new BadRequestException("تکرار رمز عبور صحیح نمی‌باشد");
            }

            var Token = await _authenticationRepository.GetTokensByResetPasswordTokenAsync(input.Token);
            if (Token == null)
            {
                throw new NotFoundException("کد صحیح نمی باشد");
            }

            if (Token.ResetPasswordTokenExpires < DateTime.Now)
            {
                throw new BadRequestException("کد منسوخ شده است. لطفا دوباره درخواست بدهید");
            }

            var user = await _authenticationRepository.GetUserByIdAsync(Token.UserId);
            if (user == null)
            {
                throw new NotFoundException("کاربر وجود ندارد");
            }




            var oldPasswordHash = user.PasswordHash;

            var result = await _authenticationRepository.RemoveUserPasswordAsync(user);
            if (!result.Succeeded)
            {
                throw new BadRequestException("مشکلی در فرایند بازنشانی رمز عبور پیش آمد");
            }

            result = await _authenticationRepository.AddUserPasswordAsync(user, input.Password);
            if (!result.Succeeded)
            {
                user.PasswordHash = oldPasswordHash;
                await _authenticationRepository.UpdateUserAsync(user);
                throw new BadRequestException("مشکلی در فرایند بازنشانی رمز عبور پیش آمد");
            }
            
            Token.ResetPasswordTokenExpires = DateTime.Now;
            await _authenticationRepository.UpdateUserTokensAsync(Token);
            
            return user.Email;
        }

        public async Task<UserDTO> ChangePassword(ChangePassword input)
        {
            if (input.Password != input.ConfirmPassword)
            {
                throw new BadRequestException("تکرار رمز عبور صحیح نمی‌باشد");
            }

            var id = Current().Id;
            var user = await _authenticationRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("کاربر یافت نشد");
            }

            var result = await _authenticationRepository.ChangeUserPasswordAsync(user, input);
            if (!result.Succeeded)
            {
                throw new BadRequestException("مشکلی در فرایند تغییر رمز عبور پیش آمد");
            }

            var userDto =  _mapper.Map<UserDTO>(user);
            userDto.Roles = await _authenticationRepository.GetUserRolesAsync(user);
            return userDto;
        }

        public async Task<UserDTO> Login(Login input)
        {
            var result = await _authenticationRepository.SignInAsync(input);

            if (!result.Succeeded)
            {
                throw new NotFoundException("نام کاربری یا رمز عبور اشتباه است");
            }

            var user = await _authenticationRepository.GetUserByUsernameAsync(input.Username);
            var userDto = _mapper.Map<UserDTO>(user);
            userDto.Roles = await _authenticationRepository.GetUserRolesAsync(user);
            return userDto;
        }
    }
}
