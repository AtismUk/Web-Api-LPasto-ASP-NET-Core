using Microsoft.AspNetCore.Identity;
using Web_Api_LPasto_ASP_NET_Core.Database;
using Web_Api_LPasto_ASP_NET_Core.Database.Models.AuthZoneModels;
using Web_Api_LPasto_ASP_NET_Core.Services;

namespace TestAuth
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AppDbContext dbContext = new(new Microsoft.EntityFrameworkCore.DbContextOptions<AppDbContext>());

            var salt = SecurityPassword.SaltPassword();
            var passwordHash = SecurityPassword.HashPassword("123", salt);

            User user = new()
            {
                Login = "123",
                Password = passwordHash,
                Salt = salt,
            };

            dbContext.Users.Add(user);
            dbContext.SaveChanges();


        }
    }
}