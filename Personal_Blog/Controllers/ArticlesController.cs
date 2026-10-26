using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Personal_Blog.Interfaces;
using Personal_Blog.Models;

namespace Personal_Blog.Controllers;

[Authorize]
public class ArticlesController: Controller
{
    private readonly ILogger _logger;
    private readonly IArticle _article;
    private readonly Articles _articles;

    public ArticlesController(ILogger<ArticlesController> logger,IArticle article, Articles  articles)
    {
        _logger = logger;
        _article = article;
        _articles = articles;
    }

    public async Task<ActionResult> Index(int id)
    {
        // string pathFile = _config.GetConnectionString("DefaultConnection");
        var objArticle = await _article.GetArticleAsync(id);
        
        return View(objArticle);
    }
    
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> NewArticle()
    {
        return View(_articles);
    }
    
    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<ActionResult> SaveArticle(Articles article)
    {
        await _article.InsertNewArticleAsync(article);
        
        return RedirectToAction("Index", "Home");
    }
    
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> Edit(int id)
    {
        var objArticle = await _article.GetArticleAsync(id);
        
        return View(objArticle);
    }
    
    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<ActionResult> Update(Articles article)
    {

        await _article.UpdateArticleAsync(article);
        
        return RedirectToAction("Index", "Home");
    }
    
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> Delete(int id)
    {
        await _article.DeleteArticlesAsync(id);
        
        return RedirectToAction("Index", "Home");
    }
}