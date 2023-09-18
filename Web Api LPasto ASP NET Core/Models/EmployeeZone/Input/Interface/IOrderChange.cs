using Web_Api_LPasto_ASP_NET_Core.Database.Models.AuthZoneModels;
using Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Output;

namespace Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Input.Interface
{
    public interface IOrderChange
    {
        public int orderId { get; set; }
        public string Describe { get; set; }
    }
}
