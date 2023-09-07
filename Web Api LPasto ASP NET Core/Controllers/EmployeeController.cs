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


        [HttpGet("Orders")]
        public async Task<List<OrderOutput>> GetAllorders()
        {
            var res = await _employeeService.GetAllOrders();
            return res as List<OrderOutput>;
        }

        [HttpGet("Order")]
        public async Task<JsonResult> GetOrderById(int id)
        {
            try
            {
                var res = await _employeeService.GetOrderById(id);
                if (res is OrderDeliveryOutput)
                {
                    return new JsonResult(res as OrderDeliveryOutput);
                }
                else if (res is PickOrderUpOutput)
                {
                    return new JsonResult(res as PickOrderUpOutput);
                }
                else
                {
                    return new JsonResult(StatusCode(500));
                }
                
            }
            catch (Exception)
            {
                return new JsonResult(StatusCode(404));
            }

        }

    }
}
