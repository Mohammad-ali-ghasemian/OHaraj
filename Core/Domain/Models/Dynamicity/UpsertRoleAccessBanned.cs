using Microsoft.AspNetCore.Identity;
using OHaraj.Core.Domain.Entities.Management;
using Project.Application.Helpers;
using System.ComponentModel.DataAnnotations;

namespace OHaraj.Core.Domain.Models.Dynamicity
{
    public class UpsertRoleAccessBanned
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public string RoleId { get; set; }
        [Required(ErrorMessage = PublicHelper.RequiredValidationErrorMessage)]
        public int MenuId { get; set; }
    }
}
