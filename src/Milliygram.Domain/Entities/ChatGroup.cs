using Milliygram.Domain.Commons;

namespace Milliygram.Domain.Entities;

public class ChatGroup : Auditable
{
    public string Name { get; set; }
}