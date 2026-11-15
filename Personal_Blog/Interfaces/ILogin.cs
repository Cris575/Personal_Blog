using System.Security.Claims;
using Personal_Blog.Models;

namespace Personal_Blog.Interfaces;

public interface ILogin
{
    Task<User?> ValidateLogin(LoginViewModel objUser);
    ClaimsIdentity CreateClaimsIdentity(User user);
    //Task HashUsers();
    Task<User> GetUser(string email);
}