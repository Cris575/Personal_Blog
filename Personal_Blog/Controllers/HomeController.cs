using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Personal_Blog.Interfaces;
using Personal_Blog.Models;
using System.Diagnostics;

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

    [HttpGet]
    public IActionResult TestError()
    {
        throw new Exception("Error de prueba");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    [AllowAnonymous]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

        if (exceptionFeature != null)
        {
            _logger.LogError(
                exceptionFeature.Error,
                "Error no controlado en la ruta {Path}",
                exceptionFeature.Path
            );
        }

        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}