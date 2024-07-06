using Microsoft.AspNetCore.Mvc;
using Milliygram.Service.DTOs.Users;
using Milliygram.Service.Services.Chats;
using Milliygram.Service.Services.Users;
using Milliygram.Web.Models;

namespace Milliygram.Web.Controllers;

public class UsersController (IUserService userService) : Controller
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
}