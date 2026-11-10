using FoodDelivery.Entity;

namespace FoodDelivery.Service
{
    public interface IRestaurantService
    {
        IEnumerable<Restaurant> GetRestaurantList();
        Restaurant? GetRestaurantById(int id);
        Restaurant? PostRestaurant(Restaurant restaurant);
        Restaurant? PutRestaurant(int id, Restaurant restaurant);
        bool DeleteRestaurant(int id);
    }
}
