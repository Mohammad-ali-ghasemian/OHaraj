using Microsoft.AspNetCore.Identity;

namespace OHaraj.Core.Domain.Entities.Management
{
    public class Token
    {
        public int Id { get; set; }
        public string? EmailVerificationToken { get; set; }
        public DateTime? EmailVerifiedAt { get; set; }
        public string? ResetPasswordToken { get; set; }
        public DateTime? ResetPasswordTokenExpires { get; set; }

        // Navigation properties
        public IdentityUser User { get; set; }
        public string UserId { get; set; } = string.Empty;
    }
}
