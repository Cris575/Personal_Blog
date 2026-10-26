using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Personal_Blog.Interfaces;

namespace Personal_Blog.Controllers;

[Authorize(Roles = "admin")]
public class AdminController: Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IArticle _article;

    public AdminController(ILogger<HomeController> logger,IArticle article)
    {
        _logger = logger;
        _article = article;
    }
    
    public async Task<IActionResult> Index()
    {
        var articles = await _article.ReadArticlesAsync();
        return View(articles);
    }
    
 
}