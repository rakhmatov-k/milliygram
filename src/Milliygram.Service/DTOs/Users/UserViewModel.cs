using Milliygram.Service.DTOs.Assets;
using Milliygram.Service.DTOs.Chats;

namespace Milliygram.Service.DTOs.Users;

public class UserViewModel
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public AssetViewModel Picture { get; set; }
    public IEnumerable<ChatViewModel> Chats { get; set; }
}