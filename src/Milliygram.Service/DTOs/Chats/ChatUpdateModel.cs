using Milliygram.Domain.Enums;

namespace Milliygram.Service.DTOs.Chats;

public class ChatUpdateModel
{
    public long UserId { get; set; }
    public ChatType ChatType { get; set; }
}