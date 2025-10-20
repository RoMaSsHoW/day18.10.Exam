using Exam.Domain.Entities;
using Exam.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Exam.Infrastructure.Data
{
    public class Seeder
    {
        private readonly ExamDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public Seeder(ExamDbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task RunAsync()
        {
            await SeedUsersAsync();
            await SeedRestaurantsAsync();
            await SeedCouriersAsync();
            await SeedMenusAsync();
            await SeedOrdersAsync();
            await SeedOrderDetailsAsync();
        }

        private async Task SeedUsersAsync()
        {
            if (await _userManager.Users.AnyAsync()) return;

            var admin = new User()
            {
                UserName = "admin",
                Email = "admin@gmail.com",
                Role = UserRole.Admin
            };
            await _userManager.CreateAsync(admin, "Admin123");

            var user = new User()
            {
                UserName = "john",
                Email = "john@gmail.com",
                Role = UserRole.Client
            };
            await _userManager.CreateAsync(user, "User123");
        }

        private async Task SeedRestaurantsAsync()
        {
            if (await _dbContext.Restaurants.AnyAsync()) return;

            var restaurants = new List<Restaurant>
            {
                new Restaurant
                {
                    Name = "Pizza Palace",
                    Address = "123 Main St",
                    Rating = 4.7m,
                    WorkingHours = "09:00 - 22:00",
                    Description = "Лучшие пиццы в городе",
                    ContactPhone = "+1-555-1234",
                    IsActive = true,
                    MinOrderAmount = 10m,
                    DeliveryPrice = 3.5m
                },
                new Restaurant
                {
                    Name = "Sushi World",
                    Address = "456 Ocean Ave",
                    Rating = 4.5m,
                    WorkingHours = "10:00 - 21:00",
                    Description = "Свежие суши и роллы",
                    ContactPhone = "+1-555-5678",
                    IsActive = true,
                    MinOrderAmount = 15m,
                    DeliveryPrice = 4.0m
                }
            };

            await _dbContext.Restaurants.AddRangeAsync(restaurants);
            await _dbContext.SaveChangesAsync();
        }

        private async Task SeedCouriersAsync()
        {
            if (await _dbContext.Couriers.AnyAsync()) return;

            var couriers = new List<Courier>
            {
                new Courier
                {
                    UserId = 1,
                    Status = CourierStatus.OnBreak,
                    CurrentLocation = "Center City",
                    Rating = 4.8m,
                    TransportType = TransportType.Bicycle
                },
                new Courier
                {
                    UserId = 2,
                    Status = CourierStatus.OnDelivery,
                    CurrentLocation = "North Side",
                    Rating = 4.5m,
                    TransportType = TransportType.Car
                }
            };

            await _dbContext.Couriers.AddRangeAsync(couriers);
            await _dbContext.SaveChangesAsync();
        }

        private async Task SeedMenusAsync()
        {
            if (await _dbContext.Menus.AnyAsync()) return;

            var pizzaPalace = await _dbContext.Restaurants.FirstOrDefaultAsync(r => r.Name == "Pizza Palace");
            var sushiWorld = await _dbContext.Restaurants.FirstOrDefaultAsync(r => r.Name == "Sushi World");

            if (pizzaPalace is null || sushiWorld is null) return;

            var menus = new List<Menu>
            {
                new Menu
                {
                    RestaurantId = pizzaPalace.Id,
                    Name = "Pepperoni Pizza",
                    Description = "Острая пицца с колбасой и сыром",
                    Price = 12.5m,
                    Category = "Pizza",
                    IsAvailable = true,
                    PreparationTime = 20,
                    Weight = 500,
                    PhotoUrl = "images/pizza_pepperoni.jpg"
                },
                new Menu
                {
                    RestaurantId = pizzaPalace.Id,
                    Name = "Margarita Pizza",
                    Description = "Классическая пицца с томатами и базиликом",
                    Price = 10.0m,
                    Category = "Pizza",
                    IsAvailable = true,
                    PreparationTime = 18,
                    Weight = 480,
                    PhotoUrl = "images/pizza_margarita.jpg"
                },
                new Menu
                {
                    RestaurantId = sushiWorld.Id,
                    Name = "Salmon Roll",
                    Description = "Роллы с лососем и рисом",
                    Price = 8.5m,
                    Category = "Sushi",
                    IsAvailable = true,
                    PreparationTime = 10,
                    Weight = 250,
                    PhotoUrl = "images/roll_salmon.jpg"
                }
            };

            await _dbContext.Menus.AddRangeAsync(menus);
            await _dbContext.SaveChangesAsync();
        }

        private async Task SeedOrdersAsync()
        {
            if (await _dbContext.Orders.AnyAsync()) return;

            var courier = await _dbContext.Couriers.FirstOrDefaultAsync();
            var restaurant = await _dbContext.Restaurants.FirstOrDefaultAsync();
            if (courier is null || restaurant is null) return;

            var orders = new List<Order>
            {
                new Order
                {
                    UserId = 1,
                    RestaurantId = restaurant.Id,
                    CourierId = courier.Id,
                    OrderStatus = OrderStatus.Delivered,
                    CreatedAt = DateTime.UtcNow.AddHours(-5),
                    DeliveredAt = DateTime.UtcNow.AddHours(-3),
                    TotalAmount = 25.0m,
                    DeliveryAddress = "789 Sunset Blvd",
                    PaymentMethod = PaymentMethod.Card,
                    PaymentStatus = PaymentStatus.Completed
                },
                new Order
                {
                    UserId = 2,
                    RestaurantId = restaurant.Id,
                    CourierId = courier.Id,
                    OrderStatus = OrderStatus.InProgress,
                    CreatedAt = DateTime.UtcNow.AddMinutes(-45),
                    TotalAmount = 15.0m,
                    DeliveryAddress = "222 Hill St",
                    PaymentMethod = PaymentMethod.Cash,
                    PaymentStatus = PaymentStatus.Pending
                }
            };

            await _dbContext.Orders.AddRangeAsync(orders);
            await _dbContext.SaveChangesAsync();
        }

        private async Task SeedOrderDetailsAsync()
        {
            if (await _dbContext.OrderDetails.AnyAsync()) return;

            var order = await _dbContext.Orders.FirstOrDefaultAsync();
            var menuItem = await _dbContext.Menus.FirstOrDefaultAsync();

            if (order is null || menuItem is null) return;

            var details = new List<OrderDetail>
            {
                new OrderDetail
                {
                    OrderId = order.Id,
                    MenuItemId = menuItem.Id,
                    Quantity = 2,
                    Price = menuItem.Price,
                    SpecialInstructions = "Без лука"
                },
                new OrderDetail
                {
                    OrderId = order.Id,
                    MenuItemId = menuItem.Id,
                    Quantity = 1,
                    Price = menuItem.Price,
                    SpecialInstructions = "Добавить острый соус"
                }
            };

            await _dbContext.OrderDetails.AddRangeAsync(details);
            await _dbContext.SaveChangesAsync();
        }
    }
}
