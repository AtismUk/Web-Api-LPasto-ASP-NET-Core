using Web_Api_LPasto_ASP_NET_Core.Database.Models.AuthZoneModels;
using Web_Api_LPasto_ASP_NET_Core.Database.Models.CommonZone;
using Web_Api_LPasto_ASP_NET_Core.Database.Services;
using Web_Api_LPasto_ASP_NET_Core.Models.Output;
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
                        Name = user.Name,
                        Phone = user.Phone,
                        Address = order.Address,
                        Appartment = order.Appartment,
                        Entrance = order.Entrance,
                        Floor = order.Floor,
                    };
                    orderOutput.listDishes = new();
                    foreach (var orderDish in properOrderDishes)
                    {

                        orderOutput.listDishes.Add(new()
                        {
                            Name = Dish.FirstOrDefault(x => x.Id == orderDish.Id).Name,
                            Count = orderDish.Count,
                        });
                    }
                    orderOutputs.Add(orderOutput);
                }

            }
            return orderOutputs;
           
        }
    }
}
