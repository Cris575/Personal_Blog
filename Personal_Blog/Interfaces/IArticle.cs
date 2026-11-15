using Personal_Blog.Controllers;
using Personal_Blog.Models;

namespace Personal_Blog.Interfaces;

public interface IArticle
{
   Task<IEnumerable<Articles>> ReadArticlesAsync();
   Task<Articles> GetArticleAsync(int id);
   Task<bool> WriteArticlesAsync(List<Articles> articles);
   Task InsertNewArticleAsync(Articles article);
   Task UpdateArticleAsync(Articles article);
   Task DeleteArticlesAsync(int id);
   
   
   
}
