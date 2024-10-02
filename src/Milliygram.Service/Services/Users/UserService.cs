using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Milliygram.Data.UnitOfWorks;
using Milliygram.Domain.Entities.Users;
using Milliygram.Service.DTOs.Assets;
using Milliygram.Service.DTOs.UserDetails;
using Milliygram.Service.DTOs.Users;
using Milliygram.Service.Exceptions;
using Milliygram.Service.Extensions;
using Milliygram.Service.Helpers;
using Milliygram.Service.Services.Assets;
using Milliygram.Service.Services.UserDetails;
using X.PagedList;

namespace Milliygram.Service.Services.Users;

public class UserService
    (IMapper mapper,
    IUnitOfWork unitOfWork,
    IMemoryCache memoryCache,
    IAssetService assetService,
    IUserDetailService userDetailService) : IUserService
{
    private readonly string cacheKey = "EmailCodeKey";
    public async Task<UserViewModel> CreateAsync(UserCreateModel createModel)
    {
        await unitOfWork.BeginTransactionAsync();
        var existUser = await unitOfWork.Users
            .SelectAsync(u => u.Email == createModel.Email || u.UserName.ToLower() == createModel.UserName.ToLower());

        if (existUser is not null)
            throw new AlreadyExistException($"User is already exist with this email {createModel.Email} or username {createModel.UserName}");

        var mappedUser = mapper.Map<User>(createModel);
        mappedUser.Password = PasswordHasher.Hash(createModel.Password);
        mappedUser.Create();
        mappedUser.PictureId = 1;
        var createdUser = await unitOfWork.Users.InsertAsync(mappedUser);
        await unitOfWork.SaveAsync();

        var userDetail = new UserDetailCreateModel
        {
            UserId = createdUser.Id
        };
        await userDetailService.CreateAsync(userDetail);
        await unitOfWork.CommitTransactionAsync();

        return mapper.Map<UserViewModel>(createdUser);
    }

    public async Task<UserViewModel> UpdateAsync(long id, UserUpdateModel updateModel)
    {
        var existUser = await unitOfWork.Users.SelectAsync(u => u.Id == id, includes: ["Picture", "Detail"])
            ?? throw new NotFoundException($"User is not found with this ID {id}");

        var alreadyExistUser = await unitOfWork.Users
            .SelectAsync(u => u.UserName.ToLower() == updateModel.UserName.ToLower() && u.Id != id);

        if (alreadyExistUser is not null)
            throw new AlreadyExistException($"User is already exist with this username {updateModel.UserName}");

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
        var existUser = await unitOfWork.Users.SelectAsync(expression: u => u.Id == id, includes: ["Chats", "Picture", "Detail"])
           ?? throw new NotFoundException($"User is not found with this ID {id}");

        return mapper.Map<UserViewModel>(existUser);
    }


    public async Task<IPagedList<UserViewModel>> GetAllAsync(int? page, string search = null)
    {
        var users = unitOfWork.Users.SelectAsQueryable(includes: ["Chats", "Picture", "Detail"]);

        if (string.IsNullOrWhiteSpace(search))
            users = users.Where(u =>
            u.FirstName.ToLower().Contains(search.ToLower()) ||
            u.LastName.ToLower().Contains(search.ToLower()) ||
            u.UserName.ToLower().Contains(search.ToLower()));

        var pagedUsers = await users.ToPagedListAsync(page ?? 1, 10);

        return mapper.Map<IPagedList<UserViewModel>>(pagedUsers);
    }

    public async Task<UserViewModel> LoginAsync(LoginModel loginModel)
    {
        var existUser = await unitOfWork.Users.SelectAsync(u => u.UserName.ToLower() == loginModel.UserName.ToLower())
            ?? throw new ArgumentIsNotValidException("UserName or Password incorrect");

        if (!PasswordHasher.Verify(loginModel.Password, existUser.Password))
            throw new ArgumentIsNotValidException("UserName or Password incorrect)");

        return mapper.Map<UserViewModel>(existUser);
    }

    public async Task<UserViewModel> UploadPictureAsync(long id, AssetCreateModel assetCreateModel)
    {
        await unitOfWork.BeginTransactionAsync();
        var existUser = await unitOfWork.Users
            .SelectAsync(user => user.Id == id, includes: ["Picture"])
            ?? throw new NotFoundException($"User is not found with this ID={id}");

        var createdPicture = await assetService.UploadAsync(assetCreateModel);

        existUser.PictureId = createdPicture.Id;
        existUser.Update();
        await unitOfWork.Users.UpdateAsync(existUser);
        await unitOfWork.SaveAsync();
        await unitOfWork.CommitTransactionAsync();

        return mapper.Map<UserViewModel>(existUser);
    }

    public Task<UserViewModel> DeletePictureAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<UserViewModel> UpdateEmailAsync(long id, string email)
    {
        var existUser = await unitOfWork.Users.SelectAsync(u => u.Id == id)
            ?? throw new NotFoundException($"User is not found with this ID {id}");

        var alreadyExistUser = await unitOfWork.Users
           .SelectAsync(u => u.Email.ToLower() == email.ToLower() && u.Id != id);

        if (alreadyExistUser is not null)
            throw new AlreadyExistException($"User is already exist with this email {email}");

        existUser.Email = email;
        existUser.Update();
        await unitOfWork.Users.UpdateAsync(existUser);
        await unitOfWork.SaveAsync();

        return mapper.Map<UserViewModel> (existUser);
    }

    public async Task<UserViewModel> ChangePasswordAsync(long id, ChangePassword changePassword)
    {
        var existUser = await unitOfWork.Users.SelectAsync(u => u.Id == id)
            ?? throw new NotFoundException($"User is not found with this ID {id}");

        if (!PasswordHasher.Verify(changePassword.Password, existUser.Password))
            throw new ArgumentIsNotValidException("Password is incorrect");

        if (changePassword.NewPassword != changePassword.ConfirmPassword)
            throw new ArgumentIsNotValidException("Confirm password is incorrect");

        existUser.Password = PasswordHasher.Hash(changePassword.NewPassword);
        existUser.Update();
        await unitOfWork.SaveAsync();

        return mapper.Map<UserViewModel>(existUser);
    }

    public async Task<bool> SendVerificationCodeAsync(ResetPasswordRequest model)
    {
        var user = await unitOfWork.Users.SelectAsync(user => user.Email == model.Email)
           ?? throw new NotFoundException($"User is not found with this email = {model.Email}");

        var random = new Random();
        var code = random.Next(10000, 99999);
        await EmailHelper.SendMessageAsync(user.Email, "ConfirmationCode", code.ToString());

        var memoryCacheOptions = new MemoryCacheEntryOptions()
             .SetSize(50)
             .SetAbsoluteExpiration(TimeSpan.FromSeconds(100))
             .SetSlidingExpiration(TimeSpan.FromSeconds(50))
             .SetPriority(CacheItemPriority.Normal);

        memoryCache.Set(cacheKey, code.ToString(), memoryCacheOptions);

        return true;
    }

    public async Task<bool> VerifyCodeAsync(VerifyResetCode model)
    {
        var user = await unitOfWork.Users.SelectAsync(user => user.Email == model.Email)
          ?? throw new NotFoundException($"User is not found with this email = {model.Email}");

        if (memoryCache.Get(cacheKey) as string == model.Code)
            return true;

        return false;
    }

    public async Task<bool> ResetPasswordAsync(ResetPasswordModel model)
    {
        var user = await unitOfWork.Users.SelectAsync(user => user.Email == model.Email)
         ?? throw new NotFoundException($"User is not found with this email = {model.Email}");

        if (model.NewPassword != model.ConfirmPassword)
            throw new ArgumentIsNotValidException("Confirm password is incorrect");

        user.Password = PasswordHasher.Hash(model.NewPassword);
        await unitOfWork.Users.UpdateAsync(user);
        await unitOfWork.SaveAsync();

        return true;
    }
}