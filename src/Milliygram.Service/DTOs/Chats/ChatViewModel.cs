using Milliygram.Domain.Enums;

namespace Milliygram.Service.DTOs.Chats;

public class ChatViewModel
{
    public long Id { get; set; }
    public ChatType ChatType { get; set; }
}