namespace Web_Api_LPasto_ASP_NET_Core.Database.Models.CommonZone
{
    public class News : BaseModel
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public int typeNewsId { get; set; }
        public TypeNews typeNews { get; set; }
    }
}
