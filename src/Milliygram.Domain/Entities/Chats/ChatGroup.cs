using Milliygram.Domain.Commons;

namespace Milliygram.Domain.Entities.Chats;

public class ChatGroup : Auditable
{
    public long ChatId { get; set; }
    public Chat Chat { get; set; }
    public string Name { get; set; }
}