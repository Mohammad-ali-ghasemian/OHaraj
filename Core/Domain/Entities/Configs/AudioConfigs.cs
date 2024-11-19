using OHaraj.Core.Domain.Entities.Settings;

namespace OHaraj.Core.Domain.Entities.Configs
{
    public class AudioConfigs
    {
        public int Id { get; set; }
        public int MaxSize { get; set; }
        public int MaxLength { get; set; }

        // Navigation Properties
        public List<AudioSettings> AudioSettings { get; set; }
    }
}
