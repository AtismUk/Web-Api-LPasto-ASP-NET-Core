namespace Web_Api_LPasto_ASP_NET_Core.Models.Input
{
    public class CreateOrder
    {
        public int userId { get; set; }
        public string Address { get; set; }
        public string Appartment { get; set; }
        public string Floor { get; set; }
        public string Entrance { get; set; }
        public bool isIntercom { get; set; }
        public bool paymentMethod { get; set; }
        public string Describe { get; set; }
        public List<BasketDish> BasketDishes { get; set; }
    }
}
