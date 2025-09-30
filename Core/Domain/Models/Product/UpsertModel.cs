using OHaraj.Core.Domain.Entities.Shop;
using Project.Application.Helpers;
using System.ComponentModel.DataAnnotations;

namespace OHaraj.Core.Domain.Models.Product
{
    public class UpsertModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public int CategoryId { get; set; }

        public IFormFile? Image { get; set; }
    }
}
