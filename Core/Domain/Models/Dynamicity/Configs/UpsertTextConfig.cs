using Project.Application.Helpers;
using System.ComponentModel.DataAnnotations;

namespace OHaraj.Core.Domain.Models.Dynamicity.Configs
{
    public class UpsertTextConfig
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string? Name { get; set; }
        public string? Font { get; set; }
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public int? Size { get; set; }
        public int? Weight { get; set; }
        public int? Opacity { get; set; }
        public string? Color { get; set; }
        public string? BackgroundColor { get; set; }
    }
}
