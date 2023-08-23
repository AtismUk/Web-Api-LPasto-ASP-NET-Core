using Microsoft.AspNetCore.DataProtection;

namespace Web_Api_LPasto_ASP_NET_Core.Services.Interfaces
{
    public interface IAuthService
    {
        bool AuthUser(string login, string password);
        bool AuthEmployee(string login, string password, string secret);
        bool Logout();

    }
}
