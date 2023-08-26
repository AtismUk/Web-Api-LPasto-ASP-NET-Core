namespace Web_Api_LPasto_ASP_NET_Core.Database.Models.AuthZoneModels
{
    public class StatusOrder : BaseModel
    {
        public string Name { get; set; }
        public List<Order> Orders { get; set; } = new();
    }
}
