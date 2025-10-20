using Project.Application.Helpers;
using System.ComponentModel.DataAnnotations;

namespace OHaraj.Core.Domain.Models.Dynamicity
{
    public class UpsertRole
    {
        string? Id { get; set; }
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        string Name { get; set; }
    }
}
