using Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Output;
using Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Output.Intefaces;

namespace Web_Api_LPasto_ASP_NET_Core.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<OrderOutput>> GetAllOrders();

        Task<IOrder> GetOrderById(int id);
    }
}
