using DNTPersianUtils.Core;
using Project.Application.Helpers;
using System.ComponentModel.DataAnnotations;

namespace OHaraj.Core.Domain.Models.Authentication
{
    public class Register
    {
        [Display(Name = "شماره تماس")]
        [ValidIranianMobileNumber(ErrorMessage = PublicHelper.NotValidValidationErrorMessage)]
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string MobileNumber { get; set; }

        [Display(Name = "ایمیل")]
        [EmailAddress]
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string Email { get; set; }

        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string Password { get; set; }

        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
