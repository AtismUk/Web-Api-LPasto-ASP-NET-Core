namespace Web_Api_LPasto_ASP_NET_Core.Database.Models
{
    public interface IUserInterface
    {
        string Login { get; set; }
        byte[] Password { get; set; }
        byte[] Salt { get; set; }
    }
}
