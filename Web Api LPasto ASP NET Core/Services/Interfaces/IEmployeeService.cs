using System.Threading.Tasks;
using Web_Api_LPasto_ASP_NET_Core.Models;
using Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Input;
using Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Input.Interface;
using Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Output;
using Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Output.Intefaces;

namespace Web_Api_LPasto_ASP_NET_Core.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<ResponseService<List<OrderOutput>>> GetAllOrders();
        Task<ResponseService<IOrder>> GetOrderById(int id);
        Task<ResponseService<bool>> ChangeOrder(IOrderChange orderChange);
        Task<ResponseService<bool>> ChangeDishOrder(ChangeOrderDishesInput dishOrderChangeInput);
        Task<ResponseService<bool>> AddDishOrder(AddDishOrderInput addDishOrderInput);
        Task<ResponseService<bool>> DelateDishOrder(int id);
        Task<ResponseService<bool>> DeleteOrder(int id);
    }
}
