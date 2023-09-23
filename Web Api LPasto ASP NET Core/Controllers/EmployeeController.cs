using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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
        public async Task<IActionResult> GetAllorders()
        {
            var res = await _employeeService.GetAllOrders();
            if (res.IsValid == true)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        [HttpGet("Order")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var res = await _employeeService.GetOrderById(id);
            if (res.IsValid == true)
            {
                return Ok(res);
            }
            return BadRequest(res);

        }

        [HttpPut("ChangeDelivery")]
        public async Task<IActionResult> ChangeOrderDelivery([FromBody]ChangeOrderDeliveryInput changeOrderDeliveryInput)
        {
            if (changeOrderDeliveryInput != null)
            {
                var res = await _employeeService.ChangeOrder(changeOrderDeliveryInput);
                if (res.IsValid == true)
                {
                    return Ok(res);
                }
            }
            return BadRequest();
        }

        [HttpPut("ChangePicIpUp")]
        public async Task<IActionResult> ChangeOrderPicItUp([FromBody]ChangeOrderPicItUpInput changeOrderPicItUpInput)
        {
            if (changeOrderPicItUpInput != null)
            {
                var res = await _employeeService.ChangeOrder(changeOrderPicItUpInput);
                if (res.IsValid == true)
                {
                    return Ok(res);
                }
            }
            return BadRequest();
        }

        [HttpPut("ChangeDishInOrder")]
        public async Task<IActionResult> ChangeDishInOrder([FromBody] ChangeOrderDishesInput dishOrderChangeInput)
        {
            if (dishOrderChangeInput != null)
            {
                var res = await _employeeService.ChangeDishOrder(dishOrderChangeInput);
                if (res.IsValid == true)
                {
                    return Ok(res);
                }
            }
            return BadRequest();
        }

        [HttpPost("AddDishInOrder")]

        public async Task<IActionResult> AddDishInOrder([FromBody] AddDishOrderInput addDishOrderInput)
        {
            if (addDishOrderInput != null)
            {
                var res = await _employeeService.AddDishOrder(addDishOrderInput);
                if (res.IsValid == true)
                {
                    return Ok(res);
                }
            }
            return BadRequest();
        }

        [HttpDelete("DeleteDishInOrder")]
        public async Task<IActionResult> DeleteDishInOrder(int dishInOrderId)
        {
            if (dishInOrderId > 0)
            {
                var res = await _employeeService.DelateDishOrder(dishInOrderId);
                if (res.IsValid == true)
                {
                    return Ok(res);
                }
            }
            return BadRequest("Id должен быть больше 0");
        }

        [HttpDelete("DeleteOrder")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (id > 0)
            {
                var res = await _employeeService.DeleteOrder(id);
                if (res.IsValid)
                {
                    return Ok(res);
                }
                return BadRequest(res);
            }
            return BadRequest("Id должен быть больше 0");
        }

    }
}
