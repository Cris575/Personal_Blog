using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Personal_Blog.Interfaces;
using Personal_Blog.Models;

namespace Personal_Blog.Repository;

public class RepositoryDataJsonUser: ILogin
{
    private readonly string pathFile;

    public RepositoryDataJsonUser(IConfiguration config)
    {
        pathFile = config.GetConnectionString("UserInformation");
    }

    public async Task<IEnumerable<User>> ReadUsersAsync()
    {
        var file = await File.ReadAllTextAsync(pathFile);
        return JsonSerializer.Deserialize<IEnumerable<User>>(file);
    }

    public async Task<(bool isValid, string role)> ValidateLogin(User objUser)
    {
        var users = await ReadUsersAsync();
    
        var user = users.FirstOrDefault(x => x.Email == objUser.Email && x.Password == objUser.Password);

        if (user == null)
        {
            return (false, string.Empty); 
        }
        
        return (true, user.Role); 
    }

    public ClaimsIdentity CreateClaimsIdentity(User user, string userRole)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, userRole),
        };

        return new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
  
    }


}