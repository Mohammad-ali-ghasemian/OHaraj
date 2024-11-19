using OHaraj.Core.Domain.Entities.Settings;

namespace OHaraj.Core.Domain.Entities.Configs
{
    public class ImageConfigs
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? MaxSize { get; set; }
        public int? MaxWidth { get; set; }
        public int? MaxHeight { get; set; }

        // Navigation Properties
        public List<ImageSettings> ImageSettings { get; set; }

    }
}
