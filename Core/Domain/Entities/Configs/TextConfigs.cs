using OHaraj.Core.Domain.Entities.Settings;

namespace OHaraj.Core.Domain.Entities.Configs
{
    public class TextConfigs
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Font { get; set; }
        public int? Size { get; set; }
        public int? Weight { get; set; }
        public int? Opacity { get; set; }
        public string? Color { get; set; }
        public string? BackgroundColor { get; set; }

        // Navigation Properties
        public List<TextSettings> TextSettings { get; set; }
    }
}
