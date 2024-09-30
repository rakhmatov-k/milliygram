namespace Milliygram.Service.DTOs.Users;

public class ChangePassword
{
    public string Password { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}