
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Web_Api_LPasto_ASP_NET_Core.Database;
using Web_Api_LPasto_ASP_NET_Core.Database.Models.AuthZoneModels;
using Web_Api_LPasto_ASP_NET_Core.Database.Models.CommonZone;
using Web_Api_LPasto_ASP_NET_Core.Database.Services;
using Web_Api_LPasto_ASP_NET_Core.Services;
using Web_Api_LPasto_ASP_NET_Core.Services.Interfaces;

namespace Web_Api_LPasto_ASP_NET_Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new()
                {
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                };
            });
            builder.Services.AddAuthorization();
            builder.Services.AddDbContext<AppDbContext>(x =>
            {
                x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IBaseRepo<TypeDish>, BaseRepo<TypeDish>>()
                .AddScoped<IBaseRepo<Dish>, BaseRepo<Dish>>()
                .AddScoped<IBaseRepo<News>, BaseRepo<News>>()
                .AddScoped<IBaseRepo<TypeNews>, BaseRepo<TypeNews>>()
                .AddScoped<IBaseRepo<Restaurant>, BaseRepo<Restaurant>>()
                .AddScoped<IBaseRepo<User>, BaseRepo<User>>()
                .AddScoped<IBaseRepo<Employee>, BaseRepo<Employee>>()
                .AddScoped<IAuthService, AuthService>()
                .AddScoped<IBaseRepo<Order>, BaseRepo<Order>>()
                .AddScoped<IBaseRepo<User>, BaseRepo<User>>()
                .AddScoped<IBaseRepo<Dish>, BaseRepo<Dish>>()
                .AddScoped<IBaseRepo<Order_Dish>, BaseRepo<Order_Dish>>()
                .AddScoped<IBaseRepo<DishOption>, BaseRepo<DishOption>>()
                .AddScoped<IEmployeeService, EmployeeService>()
                .AddScoped<IBaseRepo<TypeOrder>, BaseRepo<TypeOrder>>()
                .AddScoped<IUserService, UserService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}