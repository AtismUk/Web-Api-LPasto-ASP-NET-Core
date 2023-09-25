namespace Web_Api_LPasto_ASP_NET_Core.Models.UserZone.Output
{
    public class ShorterOrderOutput
    {
        public int orderId { get; set; }
        public DateTime DateOfCreated { get; set; }
        public string Status { get; set; }
        public int CountOfDishes { get; set; }
        public double Price { get; set; }
    }
}
