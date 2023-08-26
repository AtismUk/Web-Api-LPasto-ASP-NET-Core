using Web_Api_LPasto_ASP_NET_Core.Database.Models.AuthZoneModels;
using Web_Api_LPasto_ASP_NET_Core.Database.Services;
using Web_Api_LPasto_ASP_NET_Core.Models.Input;
using Web_Api_LPasto_ASP_NET_Core.Services.Interfaces;

namespace Web_Api_LPasto_ASP_NET_Core.Services
{
    public class UserService : IUserService
    {
        private readonly IBaseRepo<Order> _OrderRepo;
        private readonly IBaseRepo<Order_Dish> _orderDishRepo;

        public UserService(IBaseRepo<Order> orderRepo, IBaseRepo<Order_Dish> orderDishRepo)
        {
            _OrderRepo = orderRepo;
            _orderDishRepo = orderDishRepo;
        }
        public async Task<bool> CreateOrder(CreateOrder createOrder)
        {
            if (createOrder != null)
            {
                try
                {
                    Order order = new()
                    {
                        userId = createOrder.userId,
                        Address = createOrder.Address,
                        Appartment = createOrder.Appartment,
                        Floor = createOrder.Floor,
                        Entrance = createOrder.Entrance,
                        isIntercom = createOrder.isIntercom,
                        Describe = createOrder.Describe,
                    };
                    var res = await _OrderRepo.AddUpdateModelAsync(order);
                    if (res)
                    {
                        foreach (var dish in createOrder.BasketDishes)
                        {
                            Order_Dish order_Dish = new()
                            {
                                orderId = order.Id,
                                dishId = dish.dishId,
                                dishOptionId = dish.dishOtionId,
                                Count = dish.Count,
                            };
                            await _orderDishRepo.AddUpdateModelAsync(order_Dish);
                        }
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }

            }
            return false;
        }
    }
}
