using Web_Api_LPasto_ASP_NET_Core.Models.Output;

namespace Web_Api_LPasto_ASP_NET_Core.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<OrderOutput>> GetAllOrders();
    }
}
