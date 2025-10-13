using Microsoft.AspNetCore.Identity;

namespace OHaraj.Core.Domain.Entities.Management
{
    public class RoleAccessBanned
    {
        public int Id { get; set; }

        // Navigation Properties
        public IdentityRole Role { get; set; }
        public string RoleId { get; set; }

        public Menu Menu {  get; set; } 
        public int MenuId { get; set; }
    }
}
