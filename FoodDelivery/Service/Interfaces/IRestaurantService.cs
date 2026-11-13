using FoodDelivery.Entity;

namespace FoodDelivery.Service.Interfaces
{
    public interface IRestaurantService
    {
        Task<IEnumerable<Restaurant>> GetRestaurantList();
        Task<Restaurant?> GetRestaurantById(int id);
        Task<Restaurant?> PostRestaurant(Restaurant restaurant);
        Task<Restaurant?> PutRestaurant(int id, Restaurant restaurant);
        Task<bool> DeleteRestaurant(int id);
    }
}
