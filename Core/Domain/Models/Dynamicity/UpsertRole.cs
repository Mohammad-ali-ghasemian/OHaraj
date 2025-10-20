using Project.Application.Helpers;
using System.ComponentModel.DataAnnotations;

namespace OHaraj.Core.Domain.Models.Dynamicity
{
    public class UpsertRole
    {
        public string Id { get; set; }
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string Name { get; set; }
    }
}
