using Dapper;
using Microsoft.Data.SqlClient;
using Personal_Blog.Interfaces;
using Personal_Blog.Models;

public class RepositoryDataSqlUser: ILogin
{
    private readonly string _connectionString;

    public RepositoryDataSqlUser(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "Connection string not found.");

        if (string.IsNullOrWhiteSpace(_connectionString))
        {
            throw new InvalidOperationException(
                "Connection string is empty.");
        }
    }

    public async Task<User> GetUser(string email)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            var objUser = await connection.QuerySingleOrDefaultAsync<User>(@"
                                    SELECT USERS.ID AS Id, USER_NAME AS Username, EMAIL_ADDRESS AS Email, PASSWORD_HASH AS PasswordHash, ROLE AS Role FROM USERS
                                    INNER JOIN ROLES 
                                    ON USERS.FK_USERS_ROLES = ROLES.ID
                                    WHERE EMAIL_ADDRESS = @Email", new { Email = email });

            return objUser;
        }
    }
}