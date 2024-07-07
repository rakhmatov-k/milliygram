using Milliygram.Domain.Commons;
using Milliygram.Domain.Entities.Chats;
using Milliygram.Domain.Entities.Commons;

namespace Milliygram.Domain.Entities.Users;

public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public long? PictureId { get; set; }
    public Asset Picture { get; set; }
    public UserDetail Detail { get; set; }
    public IEnumerable<Chat> Chats { get; set; }
    //public IEnumerable<Friend> Friends { get; set; }
}