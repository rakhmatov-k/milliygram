using Milliygram.Service.DTOs.UserDetails;
using X.PagedList;

namespace Milliygram.Service.Services.UserDetails;

public interface IUserDetailService
{
    Task<UserDetailViewModel> CreateAsync(UserDetailCreateModel createModel);
}