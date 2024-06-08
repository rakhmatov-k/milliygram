namespace Milliygram.Service.DTOs.UserDetails;

public class UserDetailViewModel
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Bio { get; set; }
    public string Location { get; set; }
    public DateTime? DateOfBirth { get; set; }
}