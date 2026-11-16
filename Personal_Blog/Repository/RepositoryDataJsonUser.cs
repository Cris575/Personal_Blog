using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Personal_Blog.Interfaces;
using Personal_Blog.Models;
using Personal_Blog.Utilities;

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

    //public async Task HashUsers()
    //{
    //    var file = await File.ReadAllTextAsync(pathFile);
    //    var a =  JsonSerializer.Deserialize<IEnumerable<User>>(file);

    //    foreach (var user in a)
    //    {
    //        user.HashPassword= HashPassword.Hash(user, user.HashPassword);
    //    }

    //    var json = JsonSerializer.Serialize(a, new JsonSerializerOptions { WriteIndented = true });
    //    await File.WriteAllTextAsync(pathFile, json);
    //}

    public async Task<User> ValidateLogin(LoginViewModel credentials)
    {

        var objUserFromDb = await GetUser(credentials.Email);

        if (objUserFromDb == null)
        {
            return null;
        }

        var result = HashPassword.Verify(objUserFromDb, credentials.Password);

        if (result == PasswordVerificationResult.Failed)
        {
            return null;
        }
        else if (result == PasswordVerificationResult.SuccessRehashNeeded)
        {
            // If the password needs to be rehashed, you can update it here
            return objUserFromDb; 
        }

        return objUserFromDb;

    }

    public ClaimsIdentity CreateClaimsIdentity(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(
                ClaimTypes.NameIdentifier,
                user.UserId.ToString()
            ),

            new Claim(
                ClaimTypes.Name,
                user.Username
            ),

            new Claim(
                ClaimTypes.Email,
                user.Email
            ),

            new Claim(
                ClaimTypes.Role,
                user.Role
            )
        };

        return new ClaimsIdentity(
            claims,
            CookieAuthenticationDefaults.AuthenticationScheme
        );
    }


    public async Task<User> GetUser(string email)
    {
        var users = await ReadUsersAsync();

        return users.FirstOrDefault(u =>
             string.Equals(
                 u.Email,
                 email.Trim(),
                 StringComparison.OrdinalIgnoreCase
             )
         );
    }

}