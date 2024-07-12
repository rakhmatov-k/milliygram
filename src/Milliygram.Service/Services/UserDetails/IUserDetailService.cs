﻿using Milliygram.Service.DTOs.UserDetails;
using X.PagedList;

namespace Milliygram.Service.Services.UserDetails;

public interface IUserDetailService
{
    Task<UserDetailViewModel> CreateAsync(UserDetailCreateModel createModel);
    Task<bool> DeleteAsync(long id);
    Task<UserDetailViewModel> GetByIdAsync(long id);
    Task<IPagedList<UserDetailViewModel>> GetAllAsync(int? page);
}