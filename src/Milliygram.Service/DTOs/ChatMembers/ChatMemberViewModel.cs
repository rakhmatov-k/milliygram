using Milliygram.Service.DTOs.Chats;
using Milliygram.Service.DTOs.Users;

namespace Milliygram.Service.DTOs.ChatMembers;

public class ChatMemberViewModel
{
    public long Id { get; set; }
    public ChatViewModel Chat { get; set; }
    public UserViewModel User { get; set; }
}