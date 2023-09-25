using Microsoft.EntityFrameworkCore;
using Web_Api_LPasto_ASP_NET_Core.Constant;
using Web_Api_LPasto_ASP_NET_Core.Database.Models.AuthZoneModels;
using Web_Api_LPasto_ASP_NET_Core.Database.Models.CommonZone;

namespace Web_Api_LPasto_ASP_NET_Core.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> contextOptions) : base(contextOptions)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=DESKTOP-77LVPFQ;Database=ApiLPasto;Trusted_Connection=True;TrustServerCertificate=True");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order_Dish>().HasOne(x => x.Order).WithMany(x => x.Order_Dishe).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<TypeOrder>().HasData(new[] { new TypeOrder() { Id = 1, Name = StaticConstant.Delivery }, new TypeOrder() { Id = 2, Name = StaticConstant.PickItUp } });
            modelBuilder.Entity<Restaurant>().HasData(new[] { new Restaurant() { Id = 1, Name = "L'pasto", Location = "Чита, ул. лермонтова, 9", Description = "Что то", isVeranda = true } });
            modelBuilder.Entity<Role>().HasData(new[] { new Role() { Id = 1, Name = "Сотрудник" } });
            modelBuilder.Entity<TypeDish>().HasData(new[] { new TypeDish() { Id = 1, Name = "Пицца", restaurantId = 1, imgGuid = "9d2568a3-6348-466a-ae9a-5b300f772490.jpg"}, 
                new TypeDish() { Id = 2, Name = "Паста", restaurantId = 1, imgGuid = "286ef8eb-fe27-405d-84d1-19f22192d1a2.jpg" }, 
                new TypeDish() { Id = 3, Name = "Салаты", restaurantId = 1, imgGuid = "0abcb51e-7e30-4efd-8691-179124dd3d84.jpg" },
                new TypeDish() { Id = 4, Name = "Закуски", restaurantId = 1, imgGuid = "8df6e10b-55c8-43ee-a21e-6f3e7c5a2a94.jpg"},
                new TypeDish() { Id = 5, Name = "Супы", restaurantId = 1, imgGuid = "19043b65-f9c3-4bc9-bb35-8533818d3277.jpg"},
                new TypeDish() { Id = 6, Name = "Горячее", restaurantId = 1, imgGuid = "618906d7-6e00-4e5f-b426-8ec862641a42.jpg"},
                new TypeDish() { Id = 7, Name = "Море", restaurantId = 1, imgGuid = "3a5a5f91-b44c-40f1-ba44-2b8726757ede.jpg"},
                new TypeDish() { Id = 8, Name = "Десерты", restaurantId = 1, imgGuid = "be1de608-3b37-4716-b5ca-1f9b8123f7e4.jpg"},
                new TypeDish() { Id = 9, Name = "Гарниры", restaurantId = 1, imgGuid = "16628f7c-2b0e-4041-9255-4d4b73cd010e.jpg"},
                new TypeDish() { Id = 10, Name = "Напитки", restaurantId = 1, imgGuid = "ddfd07e0-419a-4a53-9cf0-50b6d2279617.jpg"},
                });
            modelBuilder.Entity<StatusOrder>().HasData(new[] { new StatusOrder() { Id = 1, Name = "В обработке" } });
            modelBuilder.Entity<Dish>().HasData(new[] {new Dish() { Id = 1, Name = "Пепперони", Description = "идеальное сочетание хрустящего тонкого теста, сочной томатной основы, " +
                "а также щедрого количества ароматной пепперони - тонко нарезанной итальянской колбаски из говядины с острым перцем. Каждый ингредиент, который мы используем," +
                " подобран с любовью и заботой, чтобы создать истинное вкусовое совершенство.", foodValue = 0, Squirrels = 0, Oil = 0, Carbohydrates = 0, Price = 355, Weight = 489, isFitness = false, isVegan = false, typeDishId = 1 } });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<TypeDish> TypeDishes { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<TypeNews> TypeNews { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Img> Imgs { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order_Dish> Order_Dishes { get; set; }
        public DbSet<StatusOrder> StatusOrders { get; set; }
        public DbSet<DishOption> DishOptions { get; set; }
        public DbSet<TypeOrder> TypeOrders { get; set; }
        public DbSet<Options> Options { get; set; }


    }
}
