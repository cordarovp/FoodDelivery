using FoodDelivery.Entity;
using FoodDelivery.Repository;

namespace FoodDelivery.Service
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _repository;

        public RestaurantService(IRestaurantRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Restaurant> GetRestaurantList()
        {
            return _repository.GetRestaurantList();
        }

        public Restaurant? GetRestaurantById(int id)
        {
            return _repository.GetRestaurantById(id);
        }

        public Restaurant? PostRestaurant(Restaurant restaurant)
        {
            //Todo checks
            if (restaurant == null) return null;
            if (restaurant.Id != 0) return null;

            _repository.AddRestaurant(restaurant);
            return restaurant;
        }

        public Restaurant? PutRestaurant(int id, Restaurant restaurant)
        {
            var existingRestaurant = _repository.GetRestaurantById(id);
            //Todo checks and returns of correct type for status codes
            if (existingRestaurant == null || id != restaurant.Id) return null;

            _repository.UpdateRestaurant(restaurant);
            return restaurant;
        }

        public bool DeleteRestaurant(int id)
        {
            var existingRestaurant = _repository.GetRestaurantById(id);
            //Todo checks and returns of correct type for status codes
            if (existingRestaurant == null) return false;

            _repository.RemoveRestaurant(existingRestaurant);
            return true;
        }
    }
}
