namespace Web_Api_LPasto_ASP_NET_Core.Models.UserZone.Input
{
    public class CreateOrder
    {
        public string Address { get; set; }
        public string Appartment { get; set; }
        public string Floor { get; set; }
        public string Entrance { get; set; }
        public bool isIntercom { get; set; }
        public bool paymentMethod { get; set; }
        public string Describe { get; set; }
        public bool isDelivery {get; set;}
        public List<BasketDish> BasketDishes { get; set; }
    }
}
