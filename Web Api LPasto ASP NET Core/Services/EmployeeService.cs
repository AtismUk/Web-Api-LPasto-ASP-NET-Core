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
using Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Input.Interface;
using Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Input;
using Web_Api_LPasto_ASP_NET_Core.Models;

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
        public async Task<ResponseService<IOrder>> GetOrderById(int id)
        {
            var order = await _orderRepo.GetModelByIdAsync(new List<Expression<Func<Order, object>>>() { x => x.typeOrder, y => y.User, o => o.Order_Dishe, s => s.StatusOrder, t => t.typeOrder }, id);
            if (order == null)
            {
                return new ResponseService<IOrder>()
                {
                    IsValid = false,
                    Message = "Не найдено",
                };
            }
            // Интерфейс от которого наследются все orderOutput
            IOrder orderOutput;
            var dishes = await _dishRepo.GetAllModelsAsync(new List<Expression<Func<Dish, object>>>() { x => x.dishOptions });
            // Проверка если заказ на самовывоз
            if (order.typeOrder.Name == StaticConstant.PickItUp)
            {
                PickOrderPickItUpOutput pickOrderUpOutput = new()
                {
                    Name = order.User.Name,
                    Created = order.Created,
                    Phone = order.User.Phone,
                    orderId = order.Id,
                    statusName = order.StatusOrder.Name,
                    statusOrderId = order.statusOrderId,
                    TypeOrderName = order.typeOrder.Name,
                    Describe = order.Describe,
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
                orderDeliveryOutput.statusName = order.typeOrder.Name;
                orderOutput = orderDeliveryOutput;
            }
            else
            {
                return new ResponseService<IOrder>()
                {
                    IsValid = false,
                    Message = "Ошибка сервера",
                };
            }
            orderOutput.listDishes = new();
            foreach (var orderDish in order.Order_Dishe)
            {
                orderOutput.listDishes.Add(CollectOrderDishes(orderDish, dishes));
            }
            return new ResponseService<IOrder>()
            {
                IsValid = true,
                Result = orderOutput
            };
        }
      

        public DishOrder CollectOrderDishes(Order_Dish order_Dish, List<Dish> dishes)
        {
            DishOrder dishOrder = new()
            {
                dishOrderID = order_Dish.Id,
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

        public async Task<ResponseService<List<OrderOutput>>> GetAllOrders()
        {
            var orders = await _orderRepo.GetAllModelsAsync(new List<Expression<Func<Order, object>>>() { x => x.StatusOrder, x => x.User });
            if (orders == null)
            {
                return new ResponseService<List<OrderOutput>>()
                {
                    IsValid = false,
                    Message = "Не найдено"
                };
            }
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
            return new ResponseService<List<OrderOutput>>()
            {
                IsValid = true,
                Result = ordersOutput
            };
        }


        public async Task<ResponseService<bool>> ChangeOrder(IOrderChange orderChange)
        {
            var order = await _orderRepo.GetModelByIdAsync(new List<Expression<Func<Order, object>>>() { x=> x.Order_Dishe, s => s.StatusOrder }, orderChange.orderId);
            if ((order == null) || order.StatusOrder.Name == StaticConstant.OrderIsMade)
            {
                return new ResponseService<bool>()
                {
                    IsValid = false,
                    Message = "Не найдено"
                };
            }

            if (orderChange is ChangeOrderDeliveryInput)
            {
                Mapper.Replace(orderChange as ChangeOrderDeliveryInput, order);

            }
            else if (orderChange is ChangeOrderPicItUpInput)
            {
                Mapper.Replace(orderChange as ChangeOrderPicItUpInput, order);
            }
            else
            {
                return new ResponseService<bool>()
                {
                    IsValid = false,
                    Message = "Ошибка сервера"
                };
            }


            var res = await _orderRepo.AddUpdateModelAsync(order);
            return new ResponseService<bool>()
            {
                IsValid = true,
                Result = res
            };
        }


        public async Task<ResponseService<bool>> ChangeDishOrder(ChangeOrderDishesInput dishOrderChangeInput)
        {
            ResponseService<bool> responseService = new()
            {
                IsValid = false,
                Message = "Не найдено"
            };
            var orderDish = await _orderDishRepo.GetModelByIdAsync(dishOrderChangeInput.orderDishId);
            if ((orderDish == null))
            {
                return responseService;
            }
            var order = await _orderRepo.GetModelByIdAsync(new List<Expression<Func<Order, object>>>() { s => s.StatusOrder }, orderDish.orderId);
            if((order == null) || order.StatusOrder.Name == StaticConstant.OrderIsMade)
            {
                return responseService;
            }
            var Dish = await _dishRepo.GetModelByIdAsync(dishOrderChangeInput.dishId);
            if (Dish == null)
            {
                return responseService;
            }

            if (dishOrderChangeInput.dishOptionId != null && dishOrderChangeInput.dishOptionId > 0)
            {
                var dishOption = _dishOptionRepo.GetModelByIdAsync(dishOrderChangeInput.dishOptionId.Value);
                if (dishOption == null)
                {
                    return responseService;
                }
            }
            else if (dishOrderChangeInput.dishOptionId == 0)
            {
                dishOrderChangeInput.dishOptionId = null;
            }

            Mapper.Replace(dishOrderChangeInput, orderDish);
            var res = await _orderDishRepo.AddUpdateModelAsync(orderDish);
            if (res == false)
            {
                responseService.Message = "Ошибка сервера";
            }
            else
            {
                responseService.IsValid = true;
                responseService.Result = res;
            }
            return responseService;
        }

        public async Task<ResponseService<bool>> AddDishOrder(AddDishOrderInput addDishOrderInput)
        {
            ResponseService<bool> responseService = new()
            {
                IsValid = false,
                Message = "Не найдено"
            };
            var order = await _orderRepo.GetModelByIdAsync(new List<Expression<Func<Order, object>>>() { s => s.StatusOrder, d => d.Order_Dishe }, addDishOrderInput.orderId);
            if (order == null)
            {
                return responseService;
            }
            if (order.Order_Dishe.FirstOrDefault(x => x.dishId == addDishOrderInput.dishId) != null)
            {
                return responseService;
            }

            if ((addDishOrderInput.dishOptionId != null) && addDishOrderInput.dishOptionId > 0)
            {
                var optionDish = await _dishOptionRepo.GetModelByIdAsync(addDishOrderInput.dishOptionId.Value);
                if (optionDish == null)
                {
                    return responseService;
                }
            }
            else if(addDishOrderInput.dishOptionId == 0)
            {
                addDishOrderInput.dishOptionId = null;
            }

            Order_Dish order_Dish = Mapper.MappingModels<AddDishOrderInput, Order_Dish>(addDishOrderInput);
            var res = await _orderDishRepo.AddUpdateModelAsync(order_Dish);
            if (res == false)
            {
                responseService.Message = "Ошибка сервера";
            }
            else
            {
                responseService.IsValid = true;
                responseService.Result = res;
            }
            return responseService;
        }

        public async Task<ResponseService<bool>> DelateDishOrder(int id)
        {
            ResponseService<bool> responseService = new()
            {
                IsValid = false
            };
            var dishInOrder = await _orderDishRepo.GetModelByIdAsync(new List<Expression<Func<Order_Dish, object>>> { o => o.Order, d => d.Dish }, id);
            if ((dishInOrder == null) || (dishInOrder.Order == null) || (dishInOrder.Dish == null))
            {
                responseService.Message = "Не найдено";
                return responseService;
            }

            var res = await _orderDishRepo.DeleteModelByIdAsync(id);
            if(res == false)
            {
                responseService.Message = "Ошибка сервера";
            }
            else
            {
                responseService.IsValid = true;
                responseService.Result = res;
            }
            return responseService;
        }

        public async Task<ResponseService<bool>> DeleteOrder(int id)
        {
            ResponseService<bool> responseService = new()
            {
                IsValid = false
            };
            Order order = await _orderRepo.GetModelByIdAsync(new List<Expression<Func<Order, object>>> { d => d.Order_Dishe },id);
            if (order == null)
            {
                responseService.Message = "Не найдено";
                return responseService;
            }
            foreach (var dishInOrder in order.Order_Dishe)
            {
                await _orderDishRepo.DeleteModelByIdAsync(dishInOrder.Id);
            }
            _orderRepo.DisposeContext();
            var res = await _orderRepo.DeleteModelByIdAsync(id);
            if (res == false)
            {
                responseService.Message = "Ошибка сервера";
            }
            else
            {
                responseService.IsValid = true;
                responseService.Result = res;
            }
            return responseService;
        }

    }
} 