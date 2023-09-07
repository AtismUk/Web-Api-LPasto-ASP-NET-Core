namespace Web_Api_LPasto_ASP_NET_Core.Database.Models.CommonZone
{
    public class TypeDish : BaseModel
    {
        public string Name { get; set; }
        public string? imgGuid { get; set; }
        public int restaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
