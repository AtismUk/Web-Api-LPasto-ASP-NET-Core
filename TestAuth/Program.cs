using Microsoft.AspNetCore.Identity;
using MyMapper;
using Web_Api_LPasto_ASP_NET_Core.Database;
using Web_Api_LPasto_ASP_NET_Core.Database.Models.AuthZoneModels;
using Web_Api_LPasto_ASP_NET_Core.Database.Models.CommonZone;
using Web_Api_LPasto_ASP_NET_Core.Database.Services;
using Web_Api_LPasto_ASP_NET_Core.Models.EmployeeZone.Input;
using Web_Api_LPasto_ASP_NET_Core.Services;

namespace TestAuth
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //AppDbContext dbContext = new(new Microsoft.EntityFrameworkCore.DbContextOptions<AppDbContext>());
            //BaseRepo<Dish> orderRepo = new(dbContext);

            //var res = orderRepo.Check(new List<System.Linq.Expressions.Expression<Func<Dish, object>>>() { x => x.TypeDish});

            //Console.WriteLine(res.Result.First().TypeDish.Name);
            //Console.ReadKey();

            //var salt = SecurityPassword.SaltPassword();
            //var passwordHash = SecurityPassword.HashPassword("123", salt);

            //User user = new()
            //{
            //    Login = "123",
            //    Password = passwordHash,
            //    Salt = salt,
            //};

            //dbContext.Users.Add(user);
            //dbContext.SaveChanges();

            //user user = new()
            //{
            //    Id = 0,
            //    Name = "Anton"
            //};

            //var res = Mapper.CheckNull(user);
            //Console.WriteLine(res);
            //Console.ReadKey();

            ChangeOrderDeliveryInput changeOrderDeliveryInput = new ChangeOrderDeliveryInput()
            {
                orderId = 1,
                Address = "Новый",
                Appartment = "новый дом",
                Describe = "gghjghjghjghjghjhjghjgghjghj",
                Entrance = "3",
                Floor = "5",
                isIntercom = false,
                paymentMethod = true,
                Phone = null,
            };


            Order order = new Order()
            {
                Id = 1,
                Address = "Не новый",
                Appartment = "новый 312",
                Describe = "4",
                Entrance = "ваы",
                Floor = "аыв",
            };

            Mapper.Replace(changeOrderDeliveryInput, order);
            foreach (var propertys in order.GetType().GetProperties())
            {
                Console.WriteLine(propertys.Name + " = " + propertys.GetValue(order));
            }
            Console.ReadKey();

        }
    }

    //class user
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}