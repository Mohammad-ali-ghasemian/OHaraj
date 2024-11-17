using OHaraj.Core.Enums;

namespace OHaraj.Core.Domain.Entities.Handling
{
    public class FileManagement
    {
        public int Id { get; set; }
        public byte[] Content { get; set; } = Array.Empty<byte>();
        public FileType Type { get; set; }
        public string path { get; set; } = string.Empty;

        //Navigation Properties
        ProductId

    }
}
