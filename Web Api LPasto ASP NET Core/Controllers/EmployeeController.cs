using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_Api_LPasto_ASP_NET_Core.Database.Models.AuthZoneModels;
using Web_Api_LPasto_ASP_NET_Core.Database.Services;
using Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Input;
using Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Output;
using Web_Api_LPasto_ASP_NET_Core.Services.Interfaces;

namespace Web_Api_LPasto_ASP_NET_Core.Controllers
{
    [ApiController]
    [Authorize]
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
        public async Task<IActionResult> ChangeOrderDelivery([FromBody]ChangeOrderDeliveryInput changeOrderDeliveryInput)
        {
            if (changeOrderDeliveryInput != null)
            {
                var res = await _employeeService.ChangeOrder(changeOrderDeliveryInput);
                if (res != false)
                {
                    return new OkResult();
                }
            }
            return new BadRequestResult();
        }

        [HttpPut("ChangePicIpUp")]
        public async Task<IActionResult> ChangeOrderPicItUp([FromBody]ChangeOrderPicItUpInput changeOrderPicItUpInput)
        {
            if (changeOrderPicItUpInput != null)
            {
                var res = await _employeeService.ChangeOrder(changeOrderPicItUpInput);
                if (res != false)
                {
                    return new OkResult();
                }
            }
            return new BadRequestResult();
        }

        [HttpPut("ChangeDishInOrder")]
        public async Task<StatusCodeResult> ChangeDishInOrder([FromBody] ChangeOrderDishesInput dishOrderChangeInput)
        {
            if (dishOrderChangeInput != null)
            {
                var res = await _employeeService.ChangeDishOrder(dishOrderChangeInput);
                if (res != false)
                {
                    return new OkResult();
                }
            }
            return new BadRequestResult();
        }

        [HttpPost("AddDishInOrder")]

        public async Task<IActionResult> AddDishInOrder([FromBody] AddDishOrderInput addDishOrderInput)
        {
            if (addDishOrderInput != null)
            {
                var res = await _employeeService.AddDishOrder(addDishOrderInput);
                if (res != false)
                {
                    return new OkResult();
                }
            }
            return new BadRequestResult();
        }

        [HttpDelete("DeleteDishInOrder")]
        public async Task<IActionResult> DeleteDishInOrder(int dishInOrderId)
        {
            if (dishInOrderId > 0)
            {
                var res = await _employeeService.DelateDishOrder(dishInOrderId);
                if (res != false)
                {
                    return new OkResult();
                }
            }
            return new BadRequestResult();
        }

    }
}
