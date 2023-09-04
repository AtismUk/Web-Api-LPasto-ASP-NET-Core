using Microsoft.AspNetCore.Mvc;
using Web_Api_LPasto_ASP_NET_Core.Database.Models.AuthZoneModels;
using Web_Api_LPasto_ASP_NET_Core.Database.Services;
using Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Output;
using Web_Api_LPasto_ASP_NET_Core.Services.Interfaces;

namespace Web_Api_LPasto_ASP_NET_Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("DeliveryOrders")]
        public async Task<List<OrderDeliveryOutput>> GetDeliveryOrders()
        {
            var res = await _employeeService.GetAllDeliveryOrders();
            return res as List<OrderDeliveryOutput>;
        }

        [HttpGet("PickItUpOrders")]
        public async Task<List<PickOrderUpOutput>> GetPickItUpOrders()
        {
            var res = await _employeeService.GetAllPickOrdersUp();
            return res;
        }
    }
}
