namespace Web_Api_LPasto_ASP_NET_Core.Database.Models.CommonZone
{
    public class DishOption : BaseModel
    {
        public int dishId { get; set; }
        public string Name { get; set; }
        public Dish Dish { get; set; } = new();
    }
}
