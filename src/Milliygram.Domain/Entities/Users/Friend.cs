using Milliygram.Domain.Commons;

namespace Milliygram.Domain.Entities.Users;

public class Friend : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long UserFriendId { get; set; }
    public User UserFriend { get; set; }
}