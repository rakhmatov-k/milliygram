using Milliygram.Domain.Commons;

namespace Milliygram.Domain.Entities;

public class UserDetail : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public string? Bio {  get; set; }
    public string? Location { get; set; }
    public DateTime? DateOfBirth { get; set; }
}