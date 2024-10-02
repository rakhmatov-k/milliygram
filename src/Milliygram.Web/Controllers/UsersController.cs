using AutoMapper;
using Milliygram.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Milliygram.Service.DTOs.Users;
using Milliygram.Service.DTOs.Assets;
using Milliygram.Service.Services.Users;
using Microsoft.AspNetCore.Authentication;

namespace Milliygram.Web.Controllers;

public class UsersController 
    (IMapper mapper, 
    IUserService userService) : Controller
{
    public async Task<IActionResult> Index()
    {
        try
        {
            long userId = Convert.ToInt64(HttpContext.User.FindFirst("Id").Value);
            var user = await userService.GetByIdAsync(userId);
            return View(user); ;
        }
        catch (Exception ex)
        {
            ViewData["ServiceError"] = ex.Message;
            return View();
        }
    }

    public async Task<IActionResult> Profile()
    {
        try
        {
            long userId = Convert.ToInt64(HttpContext.User.FindFirst("Id").Value);
            var user = await userService.GetByIdAsync(userId);
            return View(user); ;
        }
        catch (Exception ex)
        {
            ViewData["ServiceError"] = ex.Message;
            return View();
        }
    }

    public async Task<IActionResult> Settings()
    {
        try
        {
            long userId = Convert.ToInt64(HttpContext.User.FindFirst("Id").Value);
            var userViewModel = await userService.GetByIdAsync(userId);
            var user = mapper.Map<UserUpdateModel>(userViewModel);
            return View(user);
        }
        catch (Exception ex)
        {
            ViewData["ServiceError"] = ex.Message;
            return View();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Settings(UserUpdateModel updateModel)
    {
        if (!ModelState.IsValid)
        {
            return View(updateModel);
        }

        try
        {
            long userId = Convert.ToInt64(HttpContext.User.FindFirst("Id").Value);
            var userViewModel = await userService.UpdateAsync(userId, updateModel);
            var user = mapper.Map<UserUpdateModel>(userViewModel);
            return View(user);
        }
        catch (Exception ex)
        {
            ViewData["ServiceError"] = ex.Message;
            return View(updateModel);
        }
    }

    public async Task<IActionResult> Security()
    {
        var userId = Convert.ToInt64(User.FindFirst("Id").Value);
        var user = await userService.GetByIdAsync(userId);
        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> ChangeEmail(string email)
    {
        try
        {
            var userId = Convert.ToInt64(User.FindFirst("Id").Value);
            await userService.UpdateEmailAsync(userId, email);
            return RedirectToAction("Security", "Users");
        }
        catch (Exception ex)
        {
            ViewData["ServiceError"] = ex.Message;
            return RedirectToAction("Security", "Users");
        }
    }

    [HttpPost]
    public async Task<IActionResult> ChangePassword(string currentpassword, string newpassword, string confirmpassword)
    {
        try
        {
            var userId = Convert.ToInt64(User.FindFirst("Id").Value);
            var changePassword = new ChangePassword
            {
                NewPassword = newpassword,
                Password = currentpassword,
                ConfirmPassword = confirmpassword
            };
            await userService.ChangePasswordAsync(userId, changePassword);
            return RedirectToAction("Security", "Users");
        }
        catch (Exception ex)
        {
            ViewData["ServiceError"] = ex.Message;
            return RedirectToAction("Security", "Users");
        }
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAccount()
    {
        try
        {
            var userId = Convert.ToInt64(User.FindFirst("Id").Value);
            await userService.DeleteAsync(userId);
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Accounts");
        }
        catch (Exception ex)
        {
            ViewData["ServiceError"] = ex.Message;
            return RedirectToAction("Security", "Users");
        }
    }

    [HttpPost]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        try
        {
            var userId = Convert.ToInt64(User.FindFirst("Id").Value);
            var asset = new AssetCreateModel
            {
                File = file,
                FileType = FileType.Images
            };
            var userViewModel = await userService.UploadPictureAsync(userId, asset);
            return RedirectToAction("Settings");
        }
        catch (Exception ex)
        {
            ViewData["ServiceError"] = ex.Message;
            return RedirectToAction("Settings");
        }
    }

    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(ResetPasswordRequest model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            await userService.SendVerificationCodeAsync(model);
            TempData["Email"] = model.Email;
            return RedirectToAction("VerifyCode");
        }
        catch (Exception ex)
        {
            ViewData["ServiceError"] = ex.Message;
            return View(model);
        }
    }

    public IActionResult VerifyCode()
    {
        var email = TempData["Email"] as string;
        if (string.IsNullOrEmpty(email))
        {
            return RedirectToAction("ForgotPassword");
        }
        var model = new VerifyResetCode { Email = email };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> VerifyCode(VerifyResetCode model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            var isValid = true; //await userService.VerifyCodeAsync(model.Email, model.Code);
            if (isValid)
            {
                TempData["Email"] = model.Email;
                return RedirectToAction("ResetPassword");
            }
            else
            {
                ModelState.AddModelError("", "Invalid verification code.");
                return View(model);
            }
        }
        catch (Exception ex)
        {
            ViewData["ServiceError"] = ex.Message;
            return View(model);
        }
    }


    public IActionResult ResetPassword()
    {
        var email = TempData["Email"] as string;
        if (string.IsNullOrEmpty(email))
        {
            return RedirectToAction("ForgotPassword");
        }
        var model = new ResetPasswordModel{ Email = email };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            //await userService.ResetPasswordAsync(model.Email, model.NewPassword);
            ViewData["Message"] = "Your password has been reset successfully.";
            return View();
        }
        catch (Exception ex)
        {
            ViewData["ServiceError"] = ex.Message;
            return View(model);
        }
    }
}