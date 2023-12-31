﻿namespace Web_Api_LPasto_ASP_NET_Core.Database.Models.AuthZoneModels
{
    public class Employee : BaseModel, IUserInterface
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
        public string Secret { get; set; }
        public int roleId { get; set; } 
        public Role Role { get; set; }
    }
}
