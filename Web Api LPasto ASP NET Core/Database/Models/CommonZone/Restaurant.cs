namespace Web_Api_LPasto_ASP_NET_Core.Database.Models.CommonZone
{
    public class Restaurant : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public bool isVeranda { get; set; }

    }
}
