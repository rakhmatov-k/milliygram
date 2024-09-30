using Milliygram.Service.DTOs.Assets;
using Milliygram.Service.DTOs.Users;
using X.PagedList;

namespace Milliygram.Service.Services.Users;

public interface IUserService
{
    Task<UserViewModel> CreateAsync(UserCreateModel createModel);
    Task<UserViewModel> UpdateAsync(long id, UserUpdateModel updateModel);
    Task<bool> DeleteAsync(long id);
    Task<UserViewModel> GetByIdAsync(long id);
    Task<IPagedList<UserViewModel>> GetAllAsync(int? page, string search = null);
    Task<UserViewModel> LoginAsync(LoginModel loginModel);
    Task<UserViewModel> UploadPictureAsync(long id, AssetCreateModel assetCreateModel);
    Task<UserViewModel> DeletePictureAsync(long id);
    Task<UserViewModel> UpdateEmailAsync(long id, string email);
    Task<UserViewModel> ChangePasswordAsync(long id, ChangePassword changePassword);
    Task<bool> SendVerificationCodeAsync(ResetPasswordRequest model);
    Task<bool> VerifyCodeAsync(VerifyResetCode model);
    Task<bool> ResetPasswordAsync(ResetPasswordModel model);
}