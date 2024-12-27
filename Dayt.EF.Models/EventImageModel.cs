using Dayt.EF.Data.Enums;

namespace Dayt.EF.Data
{
    public class EventImageModel
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public ImageTypeEnum ImageType { get; set; }
        public int Order { get; set; }

        public EventModel Event { get; set; }
    }
}