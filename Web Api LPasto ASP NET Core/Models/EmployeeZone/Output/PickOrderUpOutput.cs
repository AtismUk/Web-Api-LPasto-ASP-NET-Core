﻿using Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Output.Intefaces;

namespace Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Output
{
    public class PickOrderUpOutput : IOrderOutput
    {
        public int orderId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public List<DishOrder> listDishes { get; set; }
        public DateTime Created { get; set; }
        public int statusOrderId { get; set; }
        public string statusName { get; set; }
    }
}