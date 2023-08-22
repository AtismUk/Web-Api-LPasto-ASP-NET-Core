namespace Web_Api_LPasto_ASP_NET_Core.Database.Models.AuthZoneModels
{
    public class User : BaseModel
    {
        public string Login { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }

    }
}
