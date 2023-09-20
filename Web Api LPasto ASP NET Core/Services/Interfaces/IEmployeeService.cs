using Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Input;
using Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Input.Interface;
using Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Output;
using Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Output.Intefaces;

namespace Web_Api_LPasto_ASP_NET_Core.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<OrderOutput>> GetAllOrders();
        Task<IOrder> GetOrderById(int id);
        Task<bool> ChangeOrder(IOrderChange orderChange);
        Task<bool> ChangeDishOrder(ChangeOrderDishesInput dishOrderChangeInput);
        Task<bool> AddDishOrder(AddDishOrderInput addDishOrderInput);
        Task<bool> DelateDishOrder(int id);
    }
}
