using AutoMapper;
using Milliygram.Data.UnitOfWorks;
using Milliygram.Domain.Entities.Users;
using Milliygram.Service.DTOs.UserDetails;
using Milliygram.Service.Exceptions;
using Milliygram.Service.Extensions;

namespace Milliygram.Service.Services.UserDetails;

public class UserDetailService
    (IMapper mapper,
    IUnitOfWork unitOfWork) : IUserDetailService
{
    public async Task<UserDetailViewModel> CreateAsync(UserDetailCreateModel createModel)
    {
        var existUser = await unitOfWork.Users.SelectAsync(u => u.Id == createModel.UserId)
            ?? throw new NotFoundException($"User not found with ID {createModel.UserId}");

        var mappedUserDetail = mapper.Map<UserDetail>(createModel);
        mappedUserDetail.Create();

        var createdUserDetail = await unitOfWork.UserDetails.InsertAsync(mappedUserDetail);
        await unitOfWork.SaveAsync();

        return mapper.Map<UserDetailViewModel>(createdUserDetail);
    }
}