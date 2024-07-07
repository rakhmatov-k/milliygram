using Milliygram.Service.DTOs.Assets;
using Milliygram.Service.DTOs.Users;

namespace Milliygram.Web.Models;

public class UserModel
{
    public UserUpdateModel User { get; set; }
    public AssetViewModel Picture { get; set; }
}