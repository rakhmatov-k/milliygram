using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Milliygram.Service.Services.Chats;
using Milliygram.Web.Models;

namespace Milliygram.Web.Controllers;

[Authorize]
public class ChatsController (IChatService chatService) : Controller
{
    public async Task<IActionResult> Index(string search = null)
    {
        try
        {
            long userId = Convert.ToInt64(HttpContext.User.FindFirst("Id").Value);
            return View(new ChatListModel
            {
                Chats = await chatService.GetAllAsync(userId, search)
            });
        }
        catch (Exception ex)
        {
            ViewData["ServiceError"] = ex.Message;
            return View();
        }
    }
}