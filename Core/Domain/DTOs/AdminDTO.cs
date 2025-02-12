namespace OHaraj.Core.Domain.DTOs
{
    public class AdminDTO
    {
        public string Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public IEnumerable<string>? Roles { get; set; }
    }
}
