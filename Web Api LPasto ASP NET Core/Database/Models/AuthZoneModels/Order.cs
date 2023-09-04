using Web_Api_LPasto_ASP_NET_Core.Database.Models.CommonZone;

namespace Web_Api_LPasto_ASP_NET_Core.Database.Models.AuthZoneModels
{
    public class Order : BaseModel
    {
        public int userId { get; set; }
        public User User { get; set; }
        public string? Address { get; set; }
        public string? Appartment { get; set; }
        public string? Entrance { get; set; }
        public bool? isIntercom { get; set; }
        public string? Floor { get; set; }
        public int statusOrderId { get; set; }
        public string Describe { get; set; }
        public bool paymentMethod { get; set; }
        public int typeOrderId { get; set; }
        public DateTime Created { get; set; }
        public StatusOrder StatusOrder { get; set; }
        public TypeOrder typeOrder { get; set; }
        public List<Order_Dish> Order_Dishe { get; set; }
        public int restaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
