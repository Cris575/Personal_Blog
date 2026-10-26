using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Personal_Blog.Interfaces;
using Personal_Blog.Models;

namespace Personal_Blog.Controllers;

public class LoginController: Controller
{
    private readonly ILogin _login;

    public LoginController(ILogin login)
    {
        _login = login;
    }
    
    public IActionResult Index()
    {

        if (User.Identity != null && User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index","Home");
        }
        
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> ValidateUser(User user)
    {
        if (!ModelState.IsValid)
        {
            return View("Index", user);
        }

        if (user.Email != null && !string.IsNullOrEmpty(user.Email) &&
            user.Password != null && !string.IsNullOrEmpty(user.Password))
        {
            var userAttributes = await _login.ValidateLogin(user);

            if (userAttributes.isValid)
            {
                var claimsIdentity = _login.CreateClaimsIdentity(user, userAttributes.role); 
                
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity)).ConfigureAwait(false);
                
                if(userAttributes.role == "admin") return RedirectToAction("Index", "Admin");
                
                return RedirectToAction("Index", "Home");
            }
        }
        
        return View("Index");
    }

}