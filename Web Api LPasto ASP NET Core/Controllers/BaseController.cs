using Microsoft.AspNetCore.Mvc;
using Web_Api_LPasto_ASP_NET_Core.Database.Models.CommonZone;
using Web_Api_LPasto_ASP_NET_Core.Database.Services;

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

        public BaseController(IBaseRepo<TypeDish> typeDishCrud,
            IBaseRepo<Dish> dishCrud,
            IBaseRepo<News> newsCrud,
            IBaseRepo<TypeNews> typeNewsCrud,
            IBaseRepo<Restaurant> restaurantCrud)
        {
            _typeDishCrud = typeDishCrud;
            _dishCrud = dishCrud;
            _newsCrud = newsCrud;
            _typeNewsCrud = typeNewsCrud;
            _restaurantCrud = restaurantCrud;
        }

        [HttpGet]
        public async Task<JsonResult> DishCategories()
        {
            var allCategories = await _typeDishCrud.GetAllModelsAsync();
            return new JsonResult(allCategories);
        }
    }
}
