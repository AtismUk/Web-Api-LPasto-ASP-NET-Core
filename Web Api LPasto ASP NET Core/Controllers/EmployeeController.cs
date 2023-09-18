using Microsoft.AspNetCore.Mvc;
using Web_Api_LPasto_ASP_NET_Core.Database.Models.AuthZoneModels;
using Web_Api_LPasto_ASP_NET_Core.Database.Services;
using Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Input;
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
                else if (res is PickOrderPickItUpOutput)
                {
                    return new JsonResult(res as PickOrderPickItUpOutput);
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

        [HttpPut("ChangeDelivery")]
        public async Task<StatusCodeResult> ChangeOrderDelivery([FromBody]ChangeOrderDeliveryInput changeOrderDeliveryInput)
        {
            if (changeOrderDeliveryInput != null)
            {
                var res = await _employeeService.ChangeOrder(changeOrderDeliveryInput);
                if (res == false)
                {
                    return new StatusCodeResult(200);
                }
            }
            return new StatusCodeResult(500);
        }

        [HttpPut("ChangePicIpUp")]
        public async Task<StatusCodeResult> ChangeOrderPicItUp([FromBody]ChangeOrderPicItUpInput changeOrderPicItUpInput)
        {
            if (changeOrderPicItUpInput != null)
            {
                var res = await _employeeService.ChangeOrder(changeOrderPicItUpInput);
                if (res == false)
                {
                    return new StatusCodeResult(500);
                }
            }
            return new StatusCodeResult(200);
        }

        [HttpPut("ChangeDishInOrder")]
        public async Task<StatusCodeResult> ChangeDishInOrder([FromBody] ChangeOrderDishesInput dishOrderChangeInput)
        {
            if (dishOrderChangeInput != null)
            {
                var res = await _employeeService.ChangeDishOrder(dishOrderChangeInput);
                if (res == false)
                {
                    return new StatusCodeResult(500);
                }
            }
            return new StatusCodeResult(200);
        }

        [HttpPost("AddDishInOrder")]

        public async Task<StatusCodeResult> AddDishInOrder([FromBody] AddDishOrderInput addDishOrderInput)
        {
            if (addDishOrderInput != null)
            {
                var res = await _employeeService.AddDishOrder(addDishOrderInput);
                if (res == false)
                {
                    return new StatusCodeResult(500);
                }
            }
            return new StatusCodeResult(200);
        }
    }
}
