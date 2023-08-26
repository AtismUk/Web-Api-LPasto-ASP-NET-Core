using Microsoft.AspNetCore.Mvc;
using Web_Api_LPasto_ASP_NET_Core.Database.Models.AuthZoneModels;
using Web_Api_LPasto_ASP_NET_Core.Database.Services;
using Web_Api_LPasto_ASP_NET_Core.Models.Output;
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

        [HttpGet("Orders")]
        public async Task<List<OrderOutput>> GetOrders()
        {
            var res = await _employeeService.GetAllOrders();
            return res;
        }
    }
}
