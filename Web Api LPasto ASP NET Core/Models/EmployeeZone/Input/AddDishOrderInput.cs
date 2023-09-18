namespace Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Input
{
    public class AddDishOrderInput
    {
        public int dishId { get; set; }
        public int orderId { get; set; }
        public int? dishOptionId { get; set; }
        public int Count { get; set; }
    }
}
