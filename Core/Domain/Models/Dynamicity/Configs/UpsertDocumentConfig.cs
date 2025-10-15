using Project.Application.Helpers;
using System.ComponentModel.DataAnnotations;

namespace OHaraj.Core.Domain.Models.Dynamicity.Configs
{
    public class UpsertDocumentConfig
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public int MaxSize { get; set; }
    }
}
