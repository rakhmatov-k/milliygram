using Microsoft.AspNetCore.Mvc;

namespace Milliygram.Web.Controllers;

public class AccountsController : Controller
{
    public IActionResult Login()
    {
        return View();
    }
}