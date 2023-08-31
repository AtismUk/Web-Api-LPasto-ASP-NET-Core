namespace Web_Api_LPasto_ASP_NET_Core.Database.Models.AuthZoneModels
{
    public class TypeOrder : BaseModel
    {
        public string Name { get; set; }
        public List<TypeOrder> TypeOrders { get; set; }
    }
}
