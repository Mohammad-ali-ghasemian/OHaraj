using Microsoft.AspNetCore.Identity;
using OHaraj.Core.Domain.Entities.Shop;

namespace OHaraj.Core.Domain.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsApproved { get; set; }

        public string UserId { get; set; }

        public int ProductId { get; set; }
    }
}
