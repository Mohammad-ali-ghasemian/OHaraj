using OHaraj.Core.Domain.Entities.Configs;
using OHaraj.Core.Domain.Entities.Management;
using OHaraj.Core.Enums;

namespace OHaraj.Core.Domain.Entities.Settings
{
    public class AudioSettings
    {
        public int Id { get; set; }
        public Area Area { get; set; }

        // Navigation Properties

        public AudioConfigs AudioConfigs { get; set; }
        public int AudioConfigsId { get; set; }

        public Menu Menu { get; set; }
        public int MenuId { get; set; }
    }
}
