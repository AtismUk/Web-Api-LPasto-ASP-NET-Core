using Web_Api_LPasto_ASP_NET_Core.Models.Input;

namespace Web_Api_LPasto_ASP_NET_Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateOrder(CreateOrder createOrder);
    }
}
