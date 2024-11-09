using System.ComponentModel.DataAnnotations;

namespace OHaraj.Core.Enums
{
    public enum FileType
    {
        [Display(Name = "تصویر")]
        Image,
        [Display(Name = "فیلم")]
        Video,
        [Display(Name = "صوت")]
        Audio,
        [Display(Name = "سند")]
        Document
    }
}
