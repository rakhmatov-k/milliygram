using Milliygram.Domain.Commons;
using Milliygram.Domain.Entities.Users;
using Milliygram.Domain.Enums;

namespace Milliygram.Domain.Entities.Chats;

public class Chat : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public ChatType ChatType { get; set; }
    public IEnumerable<ChatGroup> ChatGroups { get; set; }
    public IEnumerable<ChatMember> ChatMembers { get; set; }
    public IEnumerable<Message> Messages { get; set; }
}