using Milliygram.Domain.Commons;
using Milliygram.Domain.Entities.Users;

namespace Milliygram.Domain.Entities.Chats;

public class ChatMember : Auditable
{
    public long ChatId { get; set; }
    public Chat Chat { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
}