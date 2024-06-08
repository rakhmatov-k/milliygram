using Milliygram.Domain.Enums;

namespace Milliygram.Service.DTOs.Messages;

public class MessageUpdateModel
{
    public long SenderId { get; set; }
    public long ChatId { get; set; }
    public string Content { get; set; }
    public MessageType MessageType { get; set; }
}