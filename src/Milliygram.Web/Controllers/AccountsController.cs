using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Milliygram.Service.DTOs.Users;
using Milliygram.Service.Services.Users;
using System.Security.Claims;

namespace Milliygram.Web.Controllers;

public class AccountsController 
    (IUserService userService,
    IConfiguration configuration) : Controller
{
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
        if(ModelState.IsValid)
        {
            try
            {
                var userName = configuration.GetSection("Admin:UserName").Value;
                var password = configuration.GetSection("Admin:Password").Value;
                if (userName == loginModel.UserName && password == loginModel.Password)
                {
                    var adminClaims = new List<Claim>
                    {
                        new("Id", "-1"),
                        new Claim("UserName", userName),
                        new Claim(ClaimTypes.Role, "Admin"),
                    };

                    var adminClaimsIdentity = new ClaimsIdentity(adminClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties adminProperties = new AuthenticationProperties()
                    {
                        AllowRefresh = true
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(adminClaimsIdentity));

                    return RedirectToAction("Index", "Users");
                }

                var user = await userService.LoginAsync(loginModel);
                var claims = new List<Claim>
                {
                    new("Id", user.Id.ToString()),
                    new Claim("UserName", user.UserName),
                    new Claim(ClaimTypes.Role, "User"),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Users");
            }
            catch (Exception ex)
            {
                ViewData["ServiceError"] = ex.Message;
                return View();
            }
        }
            return View(loginModel);
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserCreateModel model)
    {
        if(ModelState.IsValid)
        {
            try
            {
                var createdUser = await userService.CreateAsync(model);
                var claims = new List<Claim>
            {
                new Claim("Id", createdUser.Id.ToString()),
                new Claim("UserName", createdUser.UserName),
                new Claim(ClaimTypes.Role, "User"),
            };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Index", "Users");
            }
            catch (Exception ex)
            {
                ViewData["ServiceError"] = ex.Message;
                return View(model);
            }
        }
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Login");
    }
}