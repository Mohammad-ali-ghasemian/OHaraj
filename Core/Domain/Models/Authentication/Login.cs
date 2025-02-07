using DNTPersianUtils.Core;
using Project.Application.Helpers;
using System.ComponentModel.DataAnnotations;

namespace OHaraj.Core.Domain.Models.Authentication
{
    public class Login
    {
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string Username { get; set; }

        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string Password { get; set; }

        public bool rememberMe = false;
    }
}
