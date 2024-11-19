using Microsoft.AspNetCore.Identity;

namespace OHaraj.Core.Domain.Entities.Handling
{
    public class RoleAccessBanned
    {
        public int Id { get; set; }

        // Navigation Properties
        public IdentityRole Role { get; set; }
        public string RoleId { get; set; }

        public List<Menu> Menu {  get; set; } 
        public int MenuId { get; set; }
    }
}
