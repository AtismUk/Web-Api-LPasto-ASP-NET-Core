﻿using Web_Api_LPasto_ASP_NET_Core.Database.Models.AuthZoneModels;
using Web_Api_LPasto_ASP_NET_Core.Database.Models.CommonZone;
using Web_Api_LPasto_ASP_NET_Core.Database.Services;
using Web_Api_LPasto_ASP_NET_Core.Models.Input;
using Web_Api_LPasto_ASP_NET_Core.Services.Interfaces;

namespace Web_Api_LPasto_ASP_NET_Core.Services
{
    public class UserService : IUserService
    {
        private readonly IBaseRepo<Order> _OrderRepo;
        private readonly IBaseRepo<Order_Dish> _orderDishRepo;
        private readonly IBaseRepo<DishOption> _dishOptionRepo;
        private readonly IBaseRepo<Dish> _dishRepo;

        public UserService(IBaseRepo<Order> orderRepo, IBaseRepo<Order_Dish> orderDishRepo, IBaseRepo<DishOption> dishoptionRepo, IBaseRepo<Dish> dishRepo)
        {
            _OrderRepo = orderRepo;
            _orderDishRepo = orderDishRepo;
            _dishOptionRepo = dishoptionRepo;
            _dishRepo = dishRepo;
        }
        public async Task<bool> CreateOrder(CreateOrder createOrder)
        {
            if (createOrder != null)
            {
                try
                {
                    foreach(var dish in createOrder.BasketDishes)
                    {
                        if (dish.Count < 1)
                        {
                            throw new Exception("count = 0");
                        }
                        bool IsExist = await CheckDish(dish);
                        if (!IsExist)
                        {
                            throw new Exception("Блюда не существует");
                        }
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
                        statusOrderId = 1,
                        Created = System.DateTime.Now,
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
    }
}