using Milliygram.Domain.Commons;
using Milliygram.Domain.Enums;

namespace Milliygram.Domain.Entities;

public class Asset : Auditable
{
    public string Name { get; set; }
    public string Path { get; set; }
    public FileType FileType { get; set; }
}