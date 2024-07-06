using AutoMapper;
using Milliygram.Data.UnitOfWorks;
using Milliygram.Domain.Entities.Users;
using Milliygram.Service.DTOs.UserDetails;
using Milliygram.Service.Exceptions;
using Milliygram.Service.Extensions;
using X.PagedList;

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

    public async Task<UserDetailViewModel> UpdateAsync(long id, UserDetailUpdateModel updateModel)
    {
        var existUserDetail = await unitOfWork.UserDetails.SelectAsync(ud => ud.Id == id)
            ?? throw new NotFoundException($"UserDetail not found with ID {id}");

        var existUser = await unitOfWork.Users.SelectAsync(u => u.Id == updateModel.UserId)
            ?? throw new NotFoundException($"User not found with ID {updateModel.UserId}");

        mapper.Map(updateModel, existUserDetail);
        existUserDetail.Update();

        var updatedUserDetail = await unitOfWork.UserDetails.UpdateAsync(existUserDetail);
        await unitOfWork.SaveAsync();

        return mapper.Map<UserDetailViewModel>(updatedUserDetail);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existUserDetail = await unitOfWork.UserDetails.SelectAsync(ud => ud.Id == id)
            ?? throw new NotFoundException($"UserDetail not found with ID {id}");

        existUserDetail.Delete();
        await unitOfWork.UserDetails.DeleteAsync(existUserDetail);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async Task<UserDetailViewModel> GetByIdAsync(long id)
    {
        var existUserDetail = await unitOfWork.UserDetails.SelectAsync(ud => ud.Id == id)
            ?? throw new NotFoundException($"UserDetail not found with ID {id}");

        return mapper.Map<UserDetailViewModel>(existUserDetail);
    }

    public async Task<IPagedList<UserDetailViewModel>> GetAllAsync(int? page)
    {
        var userDetails = unitOfWork.UserDetails.SelectAsQueryable();

        var pagedUserDetails = await userDetails.ToPagedListAsync(page ?? 1, 10);

        return mapper.Map<IPagedList<UserDetailViewModel>>(pagedUserDetails);
    }
}