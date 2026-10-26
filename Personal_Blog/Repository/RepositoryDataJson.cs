using System.Text.Json;
using Personal_Blog.Controllers;
using Personal_Blog.Interfaces;
using Personal_Blog.Models;

namespace Personal_Blog.Repository;

public class RepositoryDataJson: IArticle
{
    private readonly string pathFile;

    public RepositoryDataJson(IConfiguration config)
    {
        pathFile = config.GetConnectionString("DefaultConnection");
    }
    
    public async Task<IEnumerable<Articles>> ReadArticlesAsync()
    {
        // try
        // {
            var file = await File.ReadAllTextAsync(pathFile);
            return  JsonSerializer.Deserialize<IEnumerable<Articles>>(file);
        // }
        // catch (JsonException ex)
        // {
        //     // El archivo no tiene un formato JSON válido
        //     // _logger.LogError(ex, "Error al deserializar el archivo JSON en {Path}", pathFile);
        //     throw;
        // }
        // catch (FileNotFoundException ex)
        // {
        //     // El archivo no existe
        //     // _logger.LogError(ex, "No se encontró el archivo en {Path}", pathFile);
        //     throw;
        // }
    }
    
    public async Task<Articles> GetArticleAsync(int id)
    {
        var articles = await ReadArticlesAsync();
        return articles.FirstOrDefault(a => a.Id == id);
    }

    public async Task InsertNewArticleAsync(Articles article)
    {
        var objArticle = await ReadArticlesAsync();
        
        var newArtcle = objArticle.ToList();

        article.Id = newArtcle.Count() + 1;

        newArtcle.Add(article);

        await WriteArticlesAsync(newArtcle);
    }
    
    public async Task UpdateArticleAsync(Articles article)
    {
        var objArticle = await ReadArticlesAsync();
        
        var newArticle = objArticle.Where(x => x.Id !=  article.Id).ToList();
        
        newArticle.Add(article);
        
        newArticle = newArticle.OrderBy(x => x.Id).ToList();
        
        await WriteArticlesAsync(newArticle);
    }
    
    public async Task DeleteArticlesAsync(int id)
    {
        var objArticle = await ReadArticlesAsync();
        
        var newArticle = objArticle.Where(x => x.Id !=  id).ToList();
   
        await WriteArticlesAsync(newArticle);
    }
    
    public async Task<bool> WriteArticlesAsync(List<Articles> articles)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        
        // string jsonString = JsonSerializer.Serialize(articles, options);
        
        File.WriteAllText(pathFile, JsonSerializer.Serialize(articles, options));
        
        return true;
    }
}