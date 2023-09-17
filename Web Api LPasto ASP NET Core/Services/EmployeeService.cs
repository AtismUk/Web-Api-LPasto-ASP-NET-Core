using Web_Api_LPasto_ASP_NET_Core.Database.Models.AuthZoneModels;
using Web_Api_LPasto_ASP_NET_Core.Database.Models.CommonZone;
using Web_Api_LPasto_ASP_NET_Core.Database.Services;
using Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Output;
using Web_Api_LPasto_ASP_NET_Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Output.Intefaces;
using System.Linq.Expressions;
using Web_Api_LPasto_ASP_NET_Core.Constant;
using MyMapper;

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
     
        // Получаем все заказы по Id
        public async Task<IOrder> GetOrderById(int id)
        {
            var order = await _orderRepo.GetModelByIdAsync(new List<Expression<Func<Order, object>>>() { x => x.typeOrder, y => y.User, o => o.Order_Dishe, s => s.StatusOrder }, id);
            if (order != null)
            {
                throw new Exception("Не найден");
            }
            // Интерфейс от которого наследются все orderOutput
            IOrder orderOutput;
            var dishes = await _dishRepo.GetAllModelsAsync(new List<Expression<Func<Dish, object>>>() { x => x.dishOptions });
            // Проверка если заказ на самовывоз
            if (order.typeOrder.Name == StaticConstant.PickItUp)
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
                orderOutput = pickOrderUpOutput;
            }
            else if(order.typeOrder.Name == StaticConstant.Delivery)
            {
                OrderDeliveryOutput orderDeliveryOutput = Mapper.MappingModels<Order, OrderDeliveryOutput>(order);
                orderDeliveryOutput.orderId = order.Id;
                orderDeliveryOutput.isIntercom = order.isIntercom.Value;
                orderDeliveryOutput.statusName = order.StatusOrder.Name;
                orderDeliveryOutput.Name = order.User.Name;
                orderDeliveryOutput.Phone = order.User.Phone;
                //OrderDeliveryOutput orderDeliveryOutput = new()
                //{
                //    orderId = order.Id,
                //    Name = order.User.Name,
                //    Phone = order.User.Phone,
                //    Address = order.Address,
                //    Appartment = order.Appartment,
                //    Entrance = order.Entrance,
                //    isIntercom = order.isIntercom.Value,
                //    Floor = order.Floor,
                //    Describe = order.Describe,
                //    Created = order.Created,
                //    statusName = order.StatusOrder.Name,
                //    statusOrderId = order.statusOrderId
                //};
                orderOutput = orderDeliveryOutput;
            }
            else
            {
                throw new Exception("505");
            }
            orderOutput.listDishes = new();
            foreach (var orderDish in order.Order_Dishe)
            {
                orderOutput.listDishes.Add(CollectOrderDishes(orderDish, dishes));
            }
            return orderOutput;
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

        public async Task<List<OrderOutput>> GetAllOrders()
        {
            var orders = await _orderRepo.GetAllModelsAsync(new List<Expression<Func<Order, object>>>() { x => x.StatusOrder, x=> x.User });
            List<OrderOutput> ordersOutput = new();
            foreach (var order in orders)
            {
                OrderOutput orderOutput = new()
                {
                    orderId = order.Id,
                    Date = order.Created,
                    Status = order.StatusOrder.Name,
                    userName = order.User.Name,
                };
                ordersOutput.Add(orderOutput);
            }
            return ordersOutput;
        }


        public async Task<bool> ChangeOrder(changeO)
        {
            var order = _orderRepo.GetModelByIdAsync(new List<Expression<Func<Order, object>>>() { }, id);
            if (order == null)
            {
                throw new Exception("404");
            }

        }

    }
} 