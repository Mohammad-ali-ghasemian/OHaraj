using Microsoft.AspNetCore.Identity;

namespace OHaraj.Core.Domain.Models.Product
{
    public class UpsertComment
    {
        public string Text { get; set; } = string.Empty;
        public int ProductId { get; set; }
    }
}
