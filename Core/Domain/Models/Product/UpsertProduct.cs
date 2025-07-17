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
        
        public int? Weight { get; set; }
        
        public int? Width { get; set; }
        
        public int? Height { get; set; }
        
        public int? Length { get; set; }
        
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public int Quantity { get; set; }
        
        public string? ShortContent { get; set; }
        
        public string? Content { get; set; }
        
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string Price { get; set; }
        
        public int? DiscountPercent { get; set; } = 0;

        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public bool IsActive { get; set; } = false;

        public IFormFile? MainImage { get; set; }

        public List<IFormFile>? OtherImages { get; set; }

        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public int CategoryId { get; set; }

        public int? ModelId { get; set; }

    }
}
