using Web_Api_LPasto_ASP_NET_Core.Models;
using Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Output;
using Web_Api_LPasto_ASP_NET_Core.Models.UserZone.Input;
using Web_Api_LPasto_ASP_NET_Core.Models.UserZone.Output;

namespace Web_Api_LPasto_ASP_NET_Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseService<bool>> CreateOrder(CreateOrder createOrder, string login);

        Task<ResponseService<List<ShorterOrderOutput>>> GetOrdersByUserLogin(string login);
    }
}
