namespace OHaraj.Core.Domain.Entities.Settings
{
    public class AudioSettings
    {
        public int Id { get; set; }

        // Navigation Properties
        public int MenuId { get; set; }
        public int AreaId { get; set; }
        public int ConfigId { get; set; }
    }
}
