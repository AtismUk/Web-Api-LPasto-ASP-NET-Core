using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Web_Api_LPasto_ASP_NET_Core.Database.Models.CommonZone;
using Web_Api_LPasto_ASP_NET_Core.Database.Services;
using Web_Api_LPasto_ASP_NET_Core.Models.Output;
using Web_Api_LPasto_ASP_NET_Core.Services.Interfaces;

namespace Web_Api_LPasto_ASP_NET_Core.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class BaseController : ControllerBase
    {
        private readonly IBaseRepo<TypeDish> _typeDishCrud;
        private readonly IBaseRepo<Dish> _dishCrud;
        private readonly IBaseRepo<News> _newsCrud;
        private readonly IBaseRepo<TypeNews> _typeNewsCrud;
        private readonly IBaseRepo<Restaurant> _restaurantCrud;
        private readonly IAuthService _authService;
        private readonly IEmployeeService _employeeService;

        public BaseController(IBaseRepo<TypeDish> typeDishCrud,
            IBaseRepo<Dish> dishCrud,
            IBaseRepo<News> newsCrud,
            IBaseRepo<TypeNews> typeNewsCrud,
            IBaseRepo<Restaurant> restaurantCrud,
            IAuthService authService,
            IEmployeeService employeeService
            )
        {
            _typeDishCrud = typeDishCrud;
            _dishCrud = dishCrud;
            _newsCrud = newsCrud;
            _typeNewsCrud = typeNewsCrud;
            _restaurantCrud = restaurantCrud;
            _authService = authService;
            _employeeService = employeeService;
        }

        [HttpGet("DishCategory")]
        public async Task<JsonResult> DishCategories()
        {
            var allCategories = await _typeDishCrud.GetAllModelsAsync();
            return new JsonResult(allCategories);
        }

        [HttpGet("MealByCategory")]
        public async Task<JsonResult> MealCategory(int id)
        {
            var allMeal = await _dishCrud.GetAllModelsAsync();
            var mealByCategory = allMeal.Where(x => x.typeDishId == id);
            return new JsonResult(mealByCategory);
        }

        [HttpGet("NewsType")]
        public async Task<JsonResult> NewsTypes()
        {
            var newsType = await _typeNewsCrud.GetAllModelsAsync();
            return new JsonResult(newsType);
        }

        [HttpGet("News")]
        public async Task<JsonResult> News()
        {
            var news = await _newsCrud.GetAllModelsAsync();
            return new JsonResult(news);
        }

        [HttpGet("NewsByCategory")]
        public async Task<JsonResult> NewsByCategory(int id)
        {
            var allNews = await _newsCrud.GetAllModelsAsync();
            var properNews = allNews.Where(x => x.typeNewsId == id);
            return new JsonResult(properNews);
        }

        [HttpGet("Auth")]
        public async Task<JsonResult> GetToken(string login, string password, string? secret, bool rememberMe)
        {
            try
            {
                var jwt = await _authService.AuthUser(login, password, secret, rememberMe);
                return new JsonResult(new JwtSecurityTokenHandler().WriteToken(jwt));
            }
            catch (Exception ex)
            {
                return new JsonResult(StatusCode(505));
            }
        }

    }
}
