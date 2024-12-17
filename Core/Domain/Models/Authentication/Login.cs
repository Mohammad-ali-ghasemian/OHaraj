using DNTPersianUtils.Core;
using Project.Application.Helpers;
using System.ComponentModel.DataAnnotations;

namespace OHaraj.Core.Domain.Models.Authentication
{
    public class Login
    {
        [ValidIranianMobileNumber(ErrorMessage = PublicHelper.NotValidValidationErrorMessage)]
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string Password { get; set; }
    }
}
