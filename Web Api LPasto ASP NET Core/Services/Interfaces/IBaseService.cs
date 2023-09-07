using Web_Api_LPasto_ASP_NET_Core.Database.Models.CommonZone;
using Web_Api_LPasto_ASP_NET_Core.Models.CommonZone.Output;

namespace Web_Api_LPasto_ASP_NET_Core.Services.Unterfaces
{
    public interface IBaseService
    {
        Task<List<RestaurantOutput>> GetAllRestaurant();

    }
}
