namespace OHaraj.Core.Domain.Entities.Handling
{
    public class Menu
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}
