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
}