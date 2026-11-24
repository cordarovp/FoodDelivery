using FoodDelivery.Data;
using FoodDelivery.Entity;
using FoodDelivery.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Repository
{
    public class RestaurantRepository : Repository<int, Restaurant>, IRestaurantRepository
    {
        private readonly double _deliveryTimeout;
        public RestaurantRepository(FoodDeliveryDbContext context, IConfiguration config) : base(context)
        {
            _deliveryTimeout = config.GetValue<double>("RestaurantSettings:DeliveryTimeout", 15);
        }

        public async Task<IEnumerable<Restaurant>> GetAllAvailableRestaurants(DateTime orderTime)
        {
            var minimumTime = orderTime.AddSeconds(-_deliveryTimeout); //Set to Seconds for testing purposes

            return await _dbSet.Where(r => r.LatestOrderTime == null
                                     || r.LatestOrderTime <= minimumTime)
                                .ToListAsync();
        }
    }
}
