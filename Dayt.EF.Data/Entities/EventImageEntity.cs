using Dayt.EF.Data.Enums;
using Dayt.EF.Entities;

namespace Dayt.EF.Data.Entities;

public class EventImageEntity
{
    public int Id { get; set; }
    public int EventId { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public ImageTypeEnum ImageType { get; set; }
    public int Order { get; set; }

    public EventEntity Event { get; set; }
}
