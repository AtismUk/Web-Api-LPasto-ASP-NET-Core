using Microsoft.AspNetCore.DataProtection;
using System.IdentityModel.Tokens.Jwt;
using Web_Api_LPasto_ASP_NET_Core.Database.Models;

namespace Web_Api_LPasto_ASP_NET_Core.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IUserInterface> ValidateUser(string login, string password, string? secret);

        Task<JwtSecurityToken> AuthUser(string login, string password, string? secret, bool rememberMe);

    }
}
