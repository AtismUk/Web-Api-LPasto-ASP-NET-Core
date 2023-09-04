using Web_Api_LPasto_ASP_NET_Core.Database.Models.AuthZoneModels;
using Web_Api_LPasto_ASP_NET_Core.Database.Models.CommonZone;
using Web_Api_LPasto_ASP_NET_Core.Database.Services;
using Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Output;
using Web_Api_LPasto_ASP_NET_Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Output.Intefaces;
using System.Linq.Expressions;

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
        public async Task<List<OrderDeliveryOutput>> GetAllDeliveryOrders()
        {
            List<OrderDeliveryOutput> orderOutputs = new();
            var ordersAll = await _orderRepo.GetAllModelsAsync(new List<Expression<Func<Order, object>>>() { x => x.typeOrder, y => y.User, o => o.Order_Dishes, s => s.StatusOrder });
            var orders = ordersAll.Where(x => x.typeOrder.Name == "Доставка");
            if (orders.Count() > 0)
            {
                var dishes = await _dishRepo.GetAllModelsAsync();
                var optionsDish = await _dishOptionRepo.GetAllModelsAsync();
                foreach (var order in orders)
                {
                    OrderDeliveryOutput orderOutput = new()
                    {
                        orderId = order.Id,
                        Name = order.User.Name,
                        Phone = order.User.Phone,
                        Address = order.Address,
                        Appartment = order.Appartment,
                        Entrance = order.Entrance,
                        isIntercom = order.isIntercom.Value,
                        Floor = order.Floor,
                        Describe = order.Describe,
                        Created = order.Created,
                        statusName = order.StatusOrder.Name,
                        statusOrderId = order.statusOrderId
                    };
                    orderOutput.listDishes = new();
                    foreach (var orderDish in order.Order_Dishes)
                    {
                        orderOutput.listDishes.Add(CollectOrderDishes(orderDish, dishes));
                    }
                    orderOutputs.Add(orderOutput);
                }

            }
            return orderOutputs;
           
        }

        public async Task<List<PickOrderUpOutput>> GetAllPickOrdersUp()
        {
            var allOrders = await _orderRepo.GetAllModelsAsync(new List<Expression<Func<Order, object>>>() { x => x.typeOrder, y => y.User, o => o.Order_Dishes, s => s.StatusOrder });
            var orders = allOrders.Where(x => x.typeOrder.Name == "Доствака");
            List<PickOrderUpOutput> pickOrderUpOutputs = new();
            if (orders.Count() > 0)
            {
                var dishes = await _dishRepo.GetAllModelsAsync(new List<Expression<Func<Dish, object>>>() { x => x.dishOptions });
                foreach(var order in orders)
                {
                    PickOrderUpOutput pickOrderUpOutput = new()
                    {
                        Name = order.User.Name,
                        Created = order.Created,
                        Phone = order.User.Phone,
                        orderId = order.Id,
                        statusName = order.StatusOrder.Name,
                        statusOrderId = order.statusOrderId
                    };
                    foreach(var orderDish in order.Order_Dishes)
                    {
                        pickOrderUpOutput.listDishes.Add(CollectOrderDishes(orderDish, dishes));
                    }
                    pickOrderUpOutputs.Add(pickOrderUpOutput);
                }
            }
            return pickOrderUpOutputs;
        }


        public DishOrder CollectOrderDishes(Order_Dish order_Dish, List<Dish> dishes)
        {
            DishOrder dishOrder = new()
            {
                dishId = order_Dish.dishId,
                Count = order_Dish.Count,
                Name = dishes.FirstOrDefault(x => x.Id == order_Dish.dishId).Name,
            };
            if (dishOrder.dishoptionId != null && dishOrder.dishoptionId != 0)
            {
                var option = dishes.FirstOrDefault(x => x.Id == order_Dish.Id).dishOptions.FirstOrDefault(x => x.Id == order_Dish.Id);
                dishOrder.optionName = option.Name;
            }
            return dishOrder;
        }
    }
} 