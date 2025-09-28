using Microsoft.AspNetCore.Identity;
using Project.Application.Helpers;
using System.ComponentModel.DataAnnotations;

namespace OHaraj.Core.Domain.Models.Product
{
    public class  UpsertComment
    {
        public int Id { get; set; }
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string Text { get; set; } = string.Empty;
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public int ProductId { get; set; }
    }
}
