﻿using Web_Api_LPasto_ASP_NET_Core.Database.Models.AuthZoneModels;

namespace Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Output
{
    public class OrderOutput
    {
        public int orderId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Appartment { get; set; }
        public string Entrance { get; set; }
        public bool isIntercom { get; set; }
        public string Floor { get; set; }
        public string Describe { get; set; }
        public DateTime Created { get; set; }
        public List<DishOrder> listDishes { get; set; } = new();
    }
}