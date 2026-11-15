using System.ComponentModel.DataAnnotations;

namespace Personal_Blog.Models;

public class Articles
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Date { get; set; }
    public string Text { get; set; }
}

public class ArticleCreateViewModel
{
    [Required(ErrorMessage = "El título es obligatorio.")]
    [StringLength(150)]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "El contenido es obligatorio.")]
    public string Text { get; set; } = string.Empty;
}