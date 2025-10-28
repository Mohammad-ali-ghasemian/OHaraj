using Microsoft.AspNetCore.Identity;
using OHaraj.Core.Domain.Entities.Management;

namespace OHaraj.Core.Domain.DTOs
{
    public class RoleAccessDTO
    {
        public int Id { get; set; }
        public string RoleId { get; set; }
        public int MenuId { get; set; }
    }
}
