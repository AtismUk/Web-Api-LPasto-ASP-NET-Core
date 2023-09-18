using Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Input.Interface;
using Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Output;

namespace Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Input
{
    public class ChangeOrderPicItUpInput : IOrderChange
    {
        public int orderId { get; set; }
        public List<DishOrder> dishOrders { get; set; }
        public string Describe { get; set; }

    }
}
