using System.Security.Claims;
using Personal_Blog.Models;

namespace Personal_Blog.Interfaces;

public interface ILogin
{
    Task<(bool isValid, string role)> ValidateLogin(User objUser);
    ClaimsIdentity CreateClaimsIdentity(User user, string userRol);
}