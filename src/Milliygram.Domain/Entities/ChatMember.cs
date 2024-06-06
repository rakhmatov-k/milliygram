using Milliygram.Domain.Commons;

namespace Milliygram.Domain.Entities;

public class ChatMember : Auditable
{
    public long ChatId { get; set; }
    public Chat Chat { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
}