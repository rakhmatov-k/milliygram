using AutoMapper;
using Milliygram.Data.UnitOfWorks;
using Milliygram.Domain.Entities.Users;
using Milliygram.Service.DTOs.Users;
using Milliygram.Service.Exceptions;
using Milliygram.Service.Extensions;
using Milliygram.Service.Helpers;
using X.PagedList;

namespace Milliygram.Service.Services.Users;

public class UserService
    (IMapper mapper,
    IUnitOfWork unitOfWork) : IUserService
{
    public async Task<UserViewModel> CreateAsync(UserCreateModel createModel)
    {
        var existUser = await unitOfWork.Users
            .SelectAsync(u => u.Email == createModel.Email && u.UserName.ToLower() == createModel.UserName.ToLower());

        if (existUser is not null)
            throw new AlreadyExistException($"User is already exist with this email {createModel.Email} or username {createModel.UserName}");

        var mappedUser = mapper.Map<User>(createModel);
        mappedUser.Password = PasswordHasher.Hash(createModel.Password);
        mappedUser.Create();

        var createdUser = await unitOfWork.Users.InsertAsync(mappedUser);
        await unitOfWork.SaveAsync();

        return mapper.Map<UserViewModel>(createdUser);
    }

    public async Task<UserViewModel> UpdateAsync(long id, UserUpdateModel updateModel)
    {
        var existUser = await unitOfWork.Users.SelectAsync(u => u.Id == id)
            ?? throw new NotFoundException($"User is not found with this ID {id}");

        var alreadyExistUser = await unitOfWork.Users
            .SelectAsync(u => u.Email == updateModel.Email && u.UserName.ToLower() == updateModel.UserName.ToLower() && u.Id != id);

        if (existUser is not null)
            throw new AlreadyExistException($"User is already exist with this email {updateModel.Email} or username {updateModel.UserName}");

        mapper.Map(updateModel, existUser);
        existUser.Update();

        var updatedUser = await unitOfWork.Users.UpdateAsync(existUser);
        await unitOfWork.SaveAsync();

        return mapper.Map<UserViewModel>(updatedUser);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existUser = await unitOfWork.Users.SelectAsync(u => u.Id == id)
           ?? throw new NotFoundException($"User is not found with this ID {id}");

        existUser.Delete();
        await unitOfWork.Users.DeleteAsync(existUser);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async Task<UserViewModel> GetByIdAsync(long id)
    {
        var existUser = await unitOfWork.Users.SelectAsync(expression: u => u.Id == id, includes: ["Chats", "Picture"])
           ?? throw new NotFoundException($"User is not found with this ID {id}");

        return mapper.Map<UserViewModel>(existUser);
    }


    public async Task<IPagedList<UserViewModel>> GetAllAsync(int? page, string search = null)
    {
        var users = unitOfWork.Users.SelectAsQueryable(includes: ["Chats", "Picture"]);

        if (string.IsNullOrWhiteSpace(search))
            users = users.Where(u =>
            u.FirstName.ToLower().Contains(search.ToLower()) ||
            u.LastName.ToLower().Contains(search.ToLower()) ||
            u.UserName.ToLower().Contains(search.ToLower()));

        var pagedUsers = await users.ToPagedListAsync(page ?? 1, 10);

        return mapper.Map<IPagedList<UserViewModel>>(pagedUsers);
    }

    public Task<UserViewModel> LoginAsync(LoginModel loginModel)
    {
        throw new NotImplementedException();
    }
}