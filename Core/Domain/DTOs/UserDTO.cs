namespace OHaraj.Core.Domain.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public bool? EmailConfirmed { get; set; }
        public IEnumerable<string>? Roles { get; set; }
    }
}
