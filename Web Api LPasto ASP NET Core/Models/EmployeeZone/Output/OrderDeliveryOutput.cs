using Web_Api_LPasto_ASP_NET_Core.Database.Models.AuthZoneModels;
using Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Output.Intefaces;

namespace Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Output
{
    public class OrderDeliveryOutput : IOrderOutput
    {
        public int orderId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Appartment { get; set; }
        public string Entrance { get; set; }
        public bool isIntercom { get; set; }
        public string Floor { get; set; }
        public string Describe { get; set; }
        public bool paymentMethod { get; set; }
        public DateTime Created { get; set; }
        public List<DishOrder> listDishes { get; set; } = new();
        public int statusOrderId { get; set; }
        public string statusName { get; set; }
    }
}
