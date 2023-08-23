using Web_Api_LPasto_ASP_NET_Core.Services.Interfaces;

namespace Web_Api_LPasto_ASP_NET_Core.Services
{
    public class AuthService : IAuthService
    {
        public bool AuthEmployee(string login, string password, string secret)
        {
            throw new NotImplementedException();
        }

        public bool AuthUser(string login, string password)
        {
            throw new NotImplementedException();
        }

        public bool Logout()
        {
            throw new NotImplementedException();
        }
    }
}
