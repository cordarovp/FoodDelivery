using FoodDelivery.Entity;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Data
{
    public static class SeedData
    {
        public static List<User> GetUsers() =>
            [
                new User {FirstName = "Admin", LastName = "Admin", GeoLat = 0, GeoLon = 0 , Username = "Admin", Password = "admin", IsAdmin = true},
                new User {FirstName = "TestName", LastName = "TestLastName", GeoLat = 90, GeoLon = 180, Username = "TestUsername", Password = "password", IsAdmin = false}
            ];

        public static List<Food> GetFoods() =>
            [
                new Food {Name = "TestFood", Price = 1.99m}
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
