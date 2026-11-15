using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Personal_Blog.Interfaces;
using Personal_Blog.Models;
using System.Security.Claims;

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
    [ValidateAntiForgeryToken]
    [EnableRateLimiting("login")]
    public async Task<IActionResult> ValidateUser(LoginViewModel userViewModel)
    {   
        if (!ModelState.IsValid)
        {
            return View("Index", userViewModel);
        }

        var userAttributes = await _login.ValidateLogin(userViewModel);

        if (userAttributes != null)
        {
            var claimsIdentity =  _login.CreateClaimsIdentity(userAttributes); 
                
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)).ConfigureAwait(false);
                
            if(userAttributes.Role == "admin") return RedirectToAction("Index", "Admin");
                
            return RedirectToAction("Index", "Home");
        }
        
        return View("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Index", "Login");
    }

}