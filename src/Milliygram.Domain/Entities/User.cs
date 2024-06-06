using Milliygram.Domain.Commons;

namespace Milliygram.Domain.Entities;

public class User :Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Phone {  get; set; }
    public string Password { get; set; }
    public long? PictureId { get; set; }
    public Asset Picture { get; set; }
}