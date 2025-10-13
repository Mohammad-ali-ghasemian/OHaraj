using Project.Application.Helpers;
using System.ComponentModel.DataAnnotations;

namespace OHaraj.Core.Domain.Models.Dynamicity
{
    public class UpsertVideoConfig
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public int? MaxSize { get; set; }
        public int? MaxWidth { get; set; }
        public int? MaxHeight { get; set; }
        public int? MaxLength { get; set; }
    }
}
