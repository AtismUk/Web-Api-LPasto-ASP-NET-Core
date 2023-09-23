using Web_Api_LPasto_ASP_NET_Core.Constant;
using Web_Api_LPasto_ASP_NET_Core.Database.Models.AuthZoneModels;
using Web_Api_LPasto_ASP_NET_Core.Database.Models.CommonZone;
using Web_Api_LPasto_ASP_NET_Core.Database.Services;
using Web_Api_LPasto_ASP_NET_Core.Models;
using Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Output;
using Web_Api_LPasto_ASP_NET_Core.Models.UserZone.Input;
using Web_Api_LPasto_ASP_NET_Core.Services.Interfaces;

namespace Web_Api_LPasto_ASP_NET_Core.Services
{
    public class UserService : IUserService
    {
        private readonly IBaseRepo<Order> _OrderRepo;
        private readonly IBaseRepo<Order_Dish> _orderDishRepo;
        private readonly IBaseRepo<DishOption> _dishOptionRepo;
        private readonly IBaseRepo<Dish> _dishRepo;
        private readonly IBaseRepo<TypeOrder> _typeOrderRepo;

        public UserService(IBaseRepo<Order> orderRepo, IBaseRepo<Order_Dish> orderDishRepo, IBaseRepo<DishOption> dishoptionRepo, IBaseRepo<Dish> dishRepo, IBaseRepo<TypeOrder> typeOrdersRepo)
        {
            _OrderRepo = orderRepo;
            _orderDishRepo = orderDishRepo;
            _dishOptionRepo = dishoptionRepo;
            _dishRepo = dishRepo;
            _typeOrderRepo = typeOrdersRepo;
        }
        public async Task<ResponseService<bool>> CreateOrder(CreateOrder createOrder)
        {
            ResponseService<bool> responseService = new()
            {
                IsValid = false,
            };
            foreach (var dish in createOrder.BasketDishes)
            {
                if (dish.Count < 1)
                {
                    responseService.Message = "Корзина пустая";
                    return responseService;
                }
                bool IsExist = await CheckDish(dish);
                if (!IsExist)
                {
                    responseService.Message = "Товар не найден";
                    return responseService;
                }
            }
            var allTypeOrders = await _typeOrderRepo.GetAllModelsAsync();
            TypeOrder typeOrder;
            if (createOrder.isDelivery)
            {
                typeOrder = allTypeOrders.FirstOrDefault(x => x.Name == StaticConstant.Delivery);
            }
            else
            {
                typeOrder = allTypeOrders.FirstOrDefault(x => x.Name == StaticConstant.PickItUp);
            }

            if (typeOrder == null)
            {
                responseService.Message = "Ошибка сервера";
                return responseService;
            }


            Order order = new()
            {
                userId = createOrder.userId,
                Address = createOrder.Address,
                Appartment = createOrder.Appartment,
                Floor = createOrder.Floor,
                Entrance = createOrder.Entrance,
                isIntercom = createOrder.isIntercom,
                Describe = createOrder.Describe,
                typeOrderId = typeOrder.Id,
                statusOrderId = 1,
                Created = System.DateTime.Now,
                restaurantId = 1
            };
            var res = await _OrderRepo.AddUpdateModelAsync(order);
            if (res == false)
            {
                responseService.Message = "Ошибка сервера";
                return responseService;
            }
            foreach (var dish in createOrder.BasketDishes)
            {
                Order_Dish order_Dish = new()
                {
                    orderId = order.Id,
                    dishId = dish.dishId,
                    Count = dish.Count,
                };
                if (dish.dishOtionId != null && dish.dishOtionId != 0)
                {
                    var optionDish = await _dishOptionRepo.GetModelByIdAsync(dish.dishOtionId.Value);
                    if (optionDish != null)
                    {
                        order_Dish.dishOptionId = optionDish.Id;
                    }
                }
                await _orderDishRepo.AddUpdateModelAsync(order_Dish);
            }

            responseService.IsValid = true;
            responseService.Result = res;
            responseService.Message = "";
            return responseService;
        }


        public async Task<bool> CheckDish(BasketDish basketDish)
        {
            var allDishes = await _dishRepo.GetAllModelsAsync();
            var properDish = allDishes.FirstOrDefault(x => x.Id == basketDish.dishId);
            if(properDish != null)
            {
                return true;
            }
            return false;
        }

        public Task<OrderDeliveryOutput> GetOrderByUserId(int id)
        {
            throw new NotImplementedException();
        }
    }
}
