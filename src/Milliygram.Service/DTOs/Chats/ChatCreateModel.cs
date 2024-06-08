using Milliygram.Domain.Enums;

namespace Milliygram.Service.DTOs.Chats;

public class ChatCreateModel
{
    public long UserId { get; set; }
    public ChatType ChatType { get; set; }
}