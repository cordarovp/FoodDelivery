using FoodDelivery.Entity;
using FoodDelivery.Repository.Interfaces;
using FoodDelivery.Service.Interfaces;

namespace FoodDelivery.Service
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRepository<int, Restaurant> _repository;
        
        public RestaurantService(IRepository<int, Restaurant> repository)
        {
            _repository = repository;
        }

        public IEnumerable<Restaurant> GetRestaurantList()
        {
            return _repository.GetAll();
        }

        public Restaurant? GetRestaurantById(int id)
        {
            return _repository.GetById(id);
        }

        public Restaurant? PostRestaurant(Restaurant restaurant)
        {
            //Todo checks
            if (restaurant == null) return null;
            if (restaurant.Id != 0) return null;

            _repository.Add(restaurant);
            return restaurant;
        }

        public Restaurant? PutRestaurant(int id, Restaurant restaurant)
        {
            var existingRestaurant = _repository.GetById(id);
            //Todo checks and returns of correct type for status codes
            if (existingRestaurant == null || id != restaurant.Id) return null;

            _repository.Update(restaurant);
            return restaurant;
        }

        public bool DeleteRestaurant(int id)
        {
            var existingRestaurant = _repository.GetById(id);
            //    //Todo checks and returns of correct type for status codes
            if (existingRestaurant == null) return false;

            _repository.Delete(existingRestaurant);
            return true;
        }
    }
}
