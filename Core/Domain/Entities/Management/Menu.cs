using OHaraj.Core.Domain.Entities.Settings;

namespace OHaraj.Core.Domain.Entities.Handling
{
    public class Menu
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;

        // Navigation Properties
        public Menu ParentMenu { get; set; }
        public int ParentId { get; set; }

        public List<RoleAccessBanned> RoleAccessBanned { get; set;}

        public List<AudioSettings> AudioSettings { get; set;}

        public List<DocumentSettings> DocumentSettings { get; set; }

        public List<ImageSettings> ImageSettings { get; set; }

        public List<VideoSettings> VideoSettings { get; set; }
    }
}
