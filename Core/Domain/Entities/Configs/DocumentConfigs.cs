using OHaraj.Core.Domain.Entities.Settings;

namespace OHaraj.Core.Domain.Entities.Configs
{
    public class DocumentConfigs
    {
        public int Id { get; set; }
        public int MaxSize { get; set; }

        // Navigation Properties
        public List<DocumentSettings> DocumentSettings { get; set; }
    }
}
