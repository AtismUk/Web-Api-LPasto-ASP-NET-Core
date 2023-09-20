﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<JsonResult> CreateOrder(CreateOrder createOrder)
        {
            var res = await _userService.CreateOrder(createOrder);
            if (res)
            {
                return new JsonResult(StatusCode(200));
            }
            return new JsonResult(StatusCode(500));
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("InfoUser")]
        public async Task<JsonResult> GetInfoUser(int id)
        {
            var userLogin = User.FindFirst("Login").Value;
            

        }
    }
}
