using Web_Api_LPasto_ASP_NET_Core.Database.Models.CommonZone;

namespace Web_Api_LPasto_ASP_NET_Core.Database.Models.AuthZoneModels
{
    public class Order_Dish : BaseModel
    {

        public int orderId { get; set; }
        public int dishId { get; set; }
        public int? dishOptionId { get; set; }

        public int Count { get; set; }
        public Order Order { get; set; } = new();
        public DishOption DishOption { get; set; } = new();
    }
}
