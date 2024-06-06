using Milliygram.Domain.Commons;
using Milliygram.Domain.Enums;

namespace Milliygram.Domain.Entities;

public class ChatGroup : Auditable
{
    public long ChatId { get; set; }
    public Chat Chat { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public Privacy Privacy { get; set; }
}