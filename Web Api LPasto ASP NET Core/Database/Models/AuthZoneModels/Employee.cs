namespace Web_Api_LPasto_ASP_NET_Core.Database.Models.AuthZoneModels
{
    public class Employee : BaseModel
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
        public byte[] Secret { get; set; }
        public int roleId { get; set; } 
    }
}
