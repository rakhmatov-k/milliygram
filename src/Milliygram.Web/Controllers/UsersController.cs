using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Milliygram.Service.DTOs.Users;
using Milliygram.Service.Services.Users;
using Milliygram.Web.Models;

namespace Milliygram.Web.Controllers;

public class UsersController (IMapper mapper, IUserService userService) : Controller
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
            var user = new UserModel
            {
                User = mapper.Map<UserUpdateModel>(userViewModel),
                Picture = userViewModel.Picture
            };
            return View(user); ;
        }
        catch (Exception ex)
        {
            ViewData["ServiceError"] = ex.Message;
            return View();
        }
    }

    [HttpPut]
    public async ValueTask<IActionResult> Settings(UserModel userModel)
    {
        if (!ModelState.IsValid)
        {
            return View(userModel);
        }

        try
        {
            long userId = Convert.ToInt64(HttpContext.User.FindFirst("Id").Value);
            await userService.UpdateAsync(userId, userModel.User);
            return View(userModel);
        }
        catch (Exception ex)
        {
            ViewData["ServiceError"] = ex.Message;
            return View(userModel);
        }
    }


    public async Task<IActionResult> Security()
    {
        var userId = Convert.ToInt64(User.FindFirst("Id").Value);
        var user = await userService.GetByIdAsync(userId);
        ViewBag.CurrentEmail = user.Email;
        return View();
    }
}