using FoodDelivery.Entity;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Data
{
    public static class SeedData
    {
        public static List<User> GetUsers() =>
            [
                new User {FirstName = "Admin", LastName = "Admin", GeoLat = 0, GeoLon = 0 , Username = "Admin", Password = "admin", IsAdmin = true},
                new User {FirstName = "Lana", LastName = "Jones", GeoLat = 45, GeoLon = 90, Username = "LanaJ", Password = "!@#123@#!", IsAdmin = false},
                new User {FirstName = "Andrew", LastName = "Smith", GeoLat = 34.44, GeoLon = 56.678, Username = "MrSmith", Password = "Smith123", IsAdmin = false}
            ];

        public static List<Food> GetFoods() =>
            [
                new Food {Name = "Bolognese", Price = 349.99m},
                new Food {Name = "Capricciosa", Price = 399.99m},
                new Food {Name = "Lasagne", Price = 499.99m},
                new Food {Name = "Fried chicken breast with Fries", Price = 559.99m},
                new Food {Name = "Beef stew", Price = 479.99m},
                new Food {Name = "Macaroni and cheese", Price = 129.99m}
            ];

        public static List<Restaurant> GetRestaurants() =>
            [
                new Restaurant {GeoLat = 0, GeoLon = 0},
                new Restaurant {GeoLat = 90, GeoLon = 180},
                new Restaurant {GeoLat = 45, GeoLon = 90}
            ];

        public static void Seed(DbContext context)
        {
            if (!context.Set<User>().Any())
                context.Set<User>().AddRange(GetUsers());
            if (!context.Set<Food>().Any())
                context.Set<Food>().AddRange(GetFoods());
            if (!context.Set<Restaurant>().Any())
                context.Set<Restaurant>().AddRange(GetRestaurants());

            context.SaveChanges();
        }
    }
}
