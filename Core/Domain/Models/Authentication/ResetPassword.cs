using Project.Application.Helpers;
using System.ComponentModel.DataAnnotations;

namespace OHaraj.Core.Domain.Models.Authentication
{
    public class ResetPassword
    {
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string Password { get; set; }

        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string Token { get; set; }
    }
}
