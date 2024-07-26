namespace Milliygram.Service.DTOs.Users;

public class VerifyResetCode
{
    public string Email { get; set; }
    public string Code { get; set; }
}