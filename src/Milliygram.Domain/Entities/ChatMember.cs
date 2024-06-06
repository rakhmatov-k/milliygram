using Milliygram.Domain.Commons;

namespace Milliygram.Domain.Entities;

public class ChatMember : Auditable
{
    public long ChatGroupId { get; set; }
    public ChatGroup ChatGroup { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
}