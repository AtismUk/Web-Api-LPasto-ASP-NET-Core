using Web_Api_LPasto_ASP_NET_Core.Database.Models.CommonZone;
using Web_Api_LPasto_ASP_NET_Core.Database.Services;
using Web_Api_LPasto_ASP_NET_Core.Models.CommonZone.Output;
using Web_Api_LPasto_ASP_NET_Core.Services.Unterfaces;

namespace Web_Api_LPasto_ASP_NET_Core.Services
{
    public class BaseService : IBaseService
    {
        private readonly IBaseRepo<Restaurant> _restaurantRepo;
        public BaseService()
        {
            
        }
        public Task<List<RestaurantOutput>> GetAllRestaurant()
        {
            throw new NotImplementedException();
        }
    }
}
