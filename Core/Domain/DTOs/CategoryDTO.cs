namespace OHaraj.Core.Domain.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public int? ParentCategory {  get; set; }
        List<int>? SubCategories { get; set; }
    }
}
