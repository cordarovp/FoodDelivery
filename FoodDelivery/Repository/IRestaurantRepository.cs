using FoodDelivery.Entity;

namespace FoodDelivery.Repository
{
    public interface IRestaurantRepository
    {
        IEnumerable<Restaurant> GetRestaurantList();
        Restaurant? GetRestaurantById(int id);
        void AddRestaurant(Restaurant restaurant);
        void UpdateRestaurant(Restaurant restaurant);
        void RemoveRestaurant(Restaurant restaurant);
    }
}
