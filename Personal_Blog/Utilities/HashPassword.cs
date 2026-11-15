using Microsoft.AspNetCore.Identity;
using Personal_Blog.Models;

namespace Personal_Blog.Utilities
{
    public class HashPassword
    {
        public static string Hash(User user, string password)
        {
            var hasher = new PasswordHasher<User>();
            return hasher.HashPassword(user, password);
        }

        public static PasswordVerificationResult Verify(User user, string password)
         {
            var hasher = new PasswordHasher<User>();
            return hasher.VerifyHashedPassword(user, user.PasswordHash, password);
        }
    }
}
