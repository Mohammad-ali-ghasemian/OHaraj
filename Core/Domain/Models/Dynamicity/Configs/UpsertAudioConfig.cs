using Project.Application.Helpers;
using System.ComponentModel.DataAnnotations;

namespace OHaraj.Core.Domain.Models.Dynamicity.Configs
{
    public class UpsertAudioConfig
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string? Name { get; set; }
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public int MaxSize { get; set; }
        public int MaxLength { get; set; }
    }
}
