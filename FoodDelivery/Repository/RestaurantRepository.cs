using FoodDelivery.Data;
using FoodDelivery.Entity;
using FoodDelivery.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Repository
{
    public class RestaurantRepository : Repository<int, Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(FoodDeliveryDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Restaurant>> GetAllAvailableRestaurants(DateTime orderTime)
        {
            var minimumTime = orderTime.AddSeconds(-15); //Set to Seconds for testing purposes

            return await _dbSet.Where(r => r.LatestOrderTime == null
                                     || r.LatestOrderTime <= minimumTime)
                                .ToListAsync();
        }
    }
}
