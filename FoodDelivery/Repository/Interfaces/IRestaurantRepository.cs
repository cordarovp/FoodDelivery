using FoodDelivery.Entity;

namespace FoodDelivery.Repository.Interfaces
{
    public interface IRestaurantRepository : IRepository<int, Restaurant>
    {
        Task<IEnumerable<Restaurant>> GetAllAvailableRestaurants(DateTime orderTime);
    }
}
