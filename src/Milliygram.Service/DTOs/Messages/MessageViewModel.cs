using Milliygram.Domain.Enums;
using Milliygram.Service.DTOs.Assets;
using Milliygram.Service.DTOs.Chats;
using Milliygram.Service.DTOs.Users;

namespace Milliygram.Service.DTOs.Messages;

public class MessageViewModel
{
    public long Id { get; set; }
    public UserViewModel Sender { get; set; }
    public ChatViewModel Chat { get; set; }
    public string Content { get; set; }
    public MessageType MessageType { get; set; }
    public AssetViewModel Asset { get; set; }
}