using Milliygram.Domain.Commons;
using Milliygram.Domain.Entities.Commons;
using Milliygram.Domain.Enums;

namespace Milliygram.Domain.Entities.Chats;

public class GroupDetail : Auditable
{
    public long GroupId { get; set; }
    public ChatGroup ChatGroup { get; set; }
    public string Link { get; set; }
    public string Description { get; set; }
    public Privacy Privacy { get; set; }
    public long PictureId { get; set; }
    public Asset Picture { get; set; }
}