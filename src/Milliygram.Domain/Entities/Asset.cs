using Milliygram.Domain.Commons;

namespace Milliygram.Domain.Entities;

public class Asset : Auditable
{
    public string Name { get; set; }
    public string Path { get; set; }
}