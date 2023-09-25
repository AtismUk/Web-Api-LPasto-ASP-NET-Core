using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web_Api_LPasto_ASP_NET_Core.Database.Models;
using Web_Api_LPasto_ASP_NET_Core.Models.UserZone.Input;
using Web_Api_LPasto_ASP_NET_Core.Services.Interfaces;

namespace Web_Api_LPasto_ASP_NET_Core.Controllers
{
    [Authorize]
    [ApiController]
    [Route("Api/User")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Order")]
        public async Task<IActionResult> CreateOrder(CreateOrder createOrder)
        {
            string login = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value.ToString();
            if (login == null)
            {
                return BadRequest();
            }
            var res = await _userService.CreateOrder(createOrder, login);
            if (res.IsValid == true)
            {
                return Ok(res);
            }
            return BadRequest(res);
        }

        [HttpGet("GetOrders")]
        public async Task<IActionResult> GetInfoUser()
        {
            string login = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value.ToString();
            if (login == null)
            {
                return BadRequest();
            }
            var res = await _userService.GetOrdersByUserLogin(login);
            if (res.IsValid == false)
            {
                return BadRequest(res);
            }
            return Ok(res);
        }
    }
}
