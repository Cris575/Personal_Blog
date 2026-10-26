using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using Personal_Blog.Interfaces;
using Personal_Blog.Models;

namespace Personal_Blog.Repository;

public class RepositoryDataJonsUser: ILogin
{
    private readonly string pathFile;

    public RepositoryDataJonsUser(IConfiguration config)
    {
        pathFile = config.GetConnectionString("UserInformation");
    }

    public async Task<IEnumerable<User>> ReadUsersAsync()
    {
        var file = await File.ReadAllTextAsync(pathFile);
        return JsonSerializer.Deserialize<IEnumerable<User>>(file);
    }

    public async Task<bool> ValidateLogin(string email, string password)
    {
        var user = await ReadUsersAsync();
        return user.Any(x => x.Email == email && x.Password == password);
    }

}