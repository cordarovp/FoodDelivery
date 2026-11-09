using FoodDelivery.Data;
using FoodDelivery.Entity;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Repository
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly FoodDeliveryDbContext _context;

        public RestaurantRepository(FoodDeliveryDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Restaurant> GetRestaurantList()
        {
            return _context.Restaurants.ToList();
        }

        public Restaurant? GetRestaurantById(int id)
        {
            return _context.Restaurants.AsNoTracking().FirstOrDefault(r => r.Id == id);
        }

        public void AddRestaurant(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();
        }

        public void UpdateRestaurant(Restaurant restaurant)
        {
            _context.Restaurants.Update(restaurant);
            _context.SaveChanges();
        }

        public void RemoveRestaurant(Restaurant restaurant)
        {
            _context.Restaurants.Remove(restaurant);
            _context.SaveChanges();
        }
    }
}
