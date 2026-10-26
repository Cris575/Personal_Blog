using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Personal_Blog.Interfaces;
using Personal_Blog.Models;

namespace Personal_Blog.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IArticle _article;

    public HomeController(ILogger<HomeController> logger,IArticle article)
    {
        _logger = logger;
        _article = article;
    }

    public async Task<IActionResult> Index()
    {
        var articles = await _article.ReadArticlesAsync();
        return View(articles);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
            );

        return RedirectToAction("Index", "Login");
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}