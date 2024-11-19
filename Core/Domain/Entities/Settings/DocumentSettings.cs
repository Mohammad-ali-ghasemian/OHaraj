using OHaraj.Core.Domain.Entities.Configs;
using OHaraj.Core.Domain.Entities.Handling;
using OHaraj.Core.Enums;

namespace OHaraj.Core.Domain.Entities.Settings
{
    public class DocumentSettings
    {
        public int Id { get; set; }
        public Area Area { get; set; }

        // Navigation Properties

        public DocumentConfigs DocumentConfigs { get; set; }
        public int DocumentConfigsId { get; set; }

        public Menu Menu { get; set; }
        public int MenuId { get; set; }
    }
}
