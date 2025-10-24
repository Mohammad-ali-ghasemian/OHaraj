using OHaraj.Core.Domain.Entities.Configs;
using OHaraj.Core.Domain.Entities.Management;
using OHaraj.Core.Enums;

namespace OHaraj.Core.Domain.Entities.Settings
{
    public class TextSettings
    {
        public int Id { get; set; }
        public Area Area { get; set; }

        // Navigation Properties
        public TextConfigs TextConfigs { get; set; }
        public int TextConfigsId { get; set; }

        public Menu Menu { get; set; }
        public int MenuId { get; set; }
    }
}
