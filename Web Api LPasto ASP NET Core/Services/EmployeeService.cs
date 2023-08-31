using Web_Api_LPasto_ASP_NET_Core.Database.Models.AuthZoneModels;
using Web_Api_LPasto_ASP_NET_Core.Database.Models.CommonZone;
using Web_Api_LPasto_ASP_NET_Core.Database.Services;
using Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Output;
using Web_Api_LPasto_ASP_NET_Core.Services.Interfaces;

namespace Web_Api_LPasto_ASP_NET_Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IBaseRepo<Order> _orderRepo;
        private readonly IBaseRepo<User> _userRepo;
        private readonly IBaseRepo<Dish> _dishRepo;
        private readonly IBaseRepo<Order_Dish> _orderDishRepo;
        private readonly IBaseRepo<DishOption> _dishOptionRepo;
        public EmployeeService(IBaseRepo<Order> orderRepo, 
            IBaseRepo<User> userRepo, 
            IBaseRepo<Dish> dishRepo,
            IBaseRepo<Order_Dish> orderDishRepo,
            IBaseRepo<DishOption> dishOptionRepo)
        {
            _orderRepo = orderRepo;
            _userRepo = userRepo;
            _dishRepo = dishRepo;
            _orderDishRepo = orderDishRepo;
            _dishOptionRepo = dishOptionRepo;
        }
        public async Task<List<OrderOutput>> GetAllOrders()
        {
            List<OrderOutput> orderOutputs = new();
            var orders = await _orderRepo.GetAllModelsAsync();
            if (orders.Count > 0)
            {
                var Dish = await _dishRepo.GetAllModelsAsync();
                var orderDishes = await _orderDishRepo.GetAllModelsAsync();
                var optionsDish = await _dishOptionRepo.GetAllModelsAsync();
                var users = await _userRepo.GetAllModelsAsync();
                foreach (var order in orders)
                {
                    var user = users.FirstOrDefault(x => x.Id == order.userId);
                    var properOrderDishes = orderDishes.Where(x => x.orderId == order.Id);
                    OrderOutput orderOutput = new()
                    {
                        orderId = order.Id,
                        Name = user.Name,
                        Phone = user.Phone,
                        Address = order.Address,
                        Appartment = order.Appartment,
                        Entrance = order.Entrance,
                        isIntercom = order.isIntercom,
                        Floor = order.Floor,
                        Describe = order.Describe,
                        Created = order.Created,
                    };
                    orderOutput.listDishes = new();
                    foreach (var orderDish in properOrderDishes)
                    {
                        DishOrder dishOrder = new()
                        {
                            dishId = orderDish.dishId,
                            Name = Dish.FirstOrDefault(x => x.Id == orderDish.dishId).Name,
                            Count = orderDish.Count,
                        };
                        if (orderDish.dishOptionId != null && orderDish.dishOptionId != 0)
                        {
                            dishOrder.dishoptionId = orderDish.dishOptionId.Value;
                            dishOrder.optionName = optionsDish.First(x => x.Id == orderDish.dishOptionId).Name;
                        }
                        orderOutput.listDishes.Add(dishOrder);
                    }
                    orderOutputs.Add(orderOutput);
                }

            }
            return orderOutputs;
           
        }
    }
}