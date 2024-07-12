using Milliygram.Domain.Commons;

namespace Milliygram.Domain.Entities.Users;

public class UserDetail : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public string Bio { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string DateOfBirth { get; set; } = string.Empty;
}