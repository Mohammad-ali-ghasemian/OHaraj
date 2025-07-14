using OHaraj.Core.Domain.Entities.Handling;
using OHaraj.Core.Domain.Entities.Shop;
using Project.Application.Helpers;
using System.ComponentModel.DataAnnotations;

namespace OHaraj.Core.Domain.Models.Product
{
    public class UpsertProduct
    {
        
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string Name { get; set; }
        
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public int? Weight { get; set; }
        
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public int? Width { get; set; }
        
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public int? Height { get; set; }
        
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public int? Length { get; set; }
        
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public int Quantity { get; set; }
        
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string? ShortContent { get; set; }
        
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string? Content { get; set; }
        
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string Price { get; set; }
        
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public int? DiscountPercent { get; set; } = 0;

        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public bool IsActive { get; set; } = false;

        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public IFormFile? MainImage { get; set; }

        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public List<IFormFile>? OtherImages { get; set; }

        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public int? ModelId { get; set; }

    }
}
