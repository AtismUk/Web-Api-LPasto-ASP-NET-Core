using Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Input.Interface;
using Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Output;

namespace Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Input
{
    public class ChangeOrderDeliveryInput : IOrderChange
    {
        public int orderId { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Appartment { get; set; }
        public string Entrance { get; set; }
        public bool isIntercom { get; set; }
        public string Floor { get; set; }
        public string Describe { get; set; }
        public bool paymentMethod { get; set; }

    }
}
