using Milliygram.Domain.Enums;

namespace Milliygram.Service.DTOs.Messages;

public class MessageViewModel
{
    public long Id { get; set; }
    public long SenderId { get; set; }
    public long ChatId { get; set; }
    public string Content { get; set; }
    public MessageType MessageType { get; set; }
}