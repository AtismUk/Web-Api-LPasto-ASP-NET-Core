namespace Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Output
{
    public class DishOrder
    {
        public int dishOrderID { get; set; }
        public int dishId { get; set; }
        public string Name { get; set; }
        public int dishoptionId { get; set; }
        public string optionName { get; set; }
        public int Count { get; set; }
    }
}
