namespace OHaraj.Core.Domain.DTOs
{
    public class MenuDTO
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}
