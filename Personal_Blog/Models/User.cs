using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Personal_Blog.Models;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [Length(minimumLength: 5, maximumLength: 10)]
    public string Password { get; set; }
    public string Role { get; set; }
}