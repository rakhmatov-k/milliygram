using Milliygram.Domain.Commons;
using Milliygram.Domain.Entities.Commons;
using Milliygram.Domain.Entities.Users;
using Milliygram.Domain.Enums;

namespace Milliygram.Domain.Entities.Chats;

public class Message : Auditable
{
    public long SenderId { get; set; }
    public User User { get; set; }
    public long ChatId {  get; set; }
    public Chat Chat { get; set; }
    public string? Content { get; set; }
    public long AssetId { get; set; }
    public Asset Asset { get; set; }
    public MessageType MessageType { get; set; }
}