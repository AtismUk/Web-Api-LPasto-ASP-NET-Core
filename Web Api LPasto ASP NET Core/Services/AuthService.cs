using Microsoft.AspNetCore.Authentication.OAuth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Web_Api_LPasto_ASP_NET_Core.Database.Models;
using Web_Api_LPasto_ASP_NET_Core.Database.Models.AuthZoneModels;
using Web_Api_LPasto_ASP_NET_Core.Database.Services;
using Web_Api_LPasto_ASP_NET_Core.Services.Interfaces;

namespace Web_Api_LPasto_ASP_NET_Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseRepo<User> _userRepo;
        private readonly IBaseRepo<Employee> _employeeRepo;
        public AuthService(IBaseRepo<User> userRepo, IBaseRepo<Employee> employeeRepo)
        {
            _userRepo = userRepo;
            _employeeRepo = employeeRepo;
        }
        public async Task<JwtSecurityToken> AuthUser(string login, string password, string? secret, bool rememberMe)
        {
            var res = await ValidateUser(login, password, secret);
            if (res == null)
            {
                throw new Exception("Пользователь не найден");
            }
            var claims = SetClaims(res);
            var jwt = GenerateToken(rememberMe, claims);
            return jwt;


        }

        public async Task<IUserInterface> ValidateUser(string login, string password, string? secret)
        {
            IEnumerable<IUserInterface> users;
            IUserInterface user;
            if (secret == null)
            {
                users = await _userRepo.GetAllModelsAsync();
            }
            else
            {
                users = await _employeeRepo.GetAllModelsAsync();
            }
            user = users.FirstOrDefault(x => x.Login == login);
            if (user == null)
            {
                throw new Exception("Не найден");
            }
            if (secret != null)
            {
                var employee = user as Employee;
                if (employee.Secret != secret)
                {
                    throw new Exception("Не найден");
                }
            }
            var passwordHash = SecurityPassword.HashPassword(password, user.Salt);
            var comparePasswords = SecurityPassword.ComparePasswords(user.Password, passwordHash);
            if (comparePasswords == false)
            {
                throw new Exception("Не верный пароль");
            }
            return user;
        }

        private List<Claim> SetClaims(IUserInterface user)
        {
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, user.Login)
            };
            return claims;
        }

        private JwtSecurityToken GenerateToken(bool remember, List<Claim> claims)
        {
            var jwt = new JwtSecurityToken(claims: claims, 
                expires: remember == true ? DateTime.UtcNow.Add(TimeSpan.FromDays(60)) : DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials()
                );
            return jwt;
        }
    }
}
