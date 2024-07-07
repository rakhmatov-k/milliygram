using Milliygram.Service.DTOs.UserDetails;

namespace Milliygram.Service.DTOs.Users;

public class UserUpdateModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public UserDetailUpdateModel Detail { get; set; }
}