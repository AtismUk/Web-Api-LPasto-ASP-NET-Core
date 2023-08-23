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

        [HttpGet]
        public async Task<JsonResult> MealByIdCategory(int id)
        {
            var allMeal = await _dishCrud.GetAllModelsAsync();
            var mealByCategory = allMeal.Where(x => x.typeDishId == id);
            return new JsonResult(mealByCategory);
        }

        [HttpGet]
        public async Task<JsonResult> NewsTypes()
        {
            var newsType = await _typeNewsCrud.GetAllModelsAsync();
            return new JsonResult(newsType);
        }

        [HttpGet]
        public async Task<JsonResult> News()
        {
            var news = await _newsCrud.GetAllModelsAsync();
            return new JsonResult(news);
        }

        [HttpGet]
        public async Task<JsonResult> NewsByIdCategory(int id)
        {
            var allNews = await _newsCrud.GetAllModelsAsync();
            var properNews = allNews.Where(x => x.typeNewsId == id);
            return new JsonResult(properNews);
        }
    }
}
