using OHaraj.Core.Domain.Entities.Configs;
using OHaraj.Core.Domain.Entities.Handling;
using OHaraj.Core.Enums;

namespace OHaraj.Core.Domain.Entities.Settings
{
    public class ImageSettings
    {
        public int Id { get; set; }
        public Area Area { get; set; }

        // Navigation Properties
        public ImageConfigs ImageConfigs { get; set; }
        public int ImageConfigsId { get; set; }

        public Menu Menu { get; set; }
        public int MenuId { get; set; }
    }
}
