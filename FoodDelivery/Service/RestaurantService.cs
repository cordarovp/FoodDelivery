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

        public async Task<IEnumerable<Restaurant>> GetRestaurantList()
        {
            return await _repository.GetAll();
        }

        public async Task<Restaurant?> GetRestaurantById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Restaurant?> PostRestaurant(Restaurant restaurant)
        {
            //Todo checks
            if (restaurant == null) return null;
            if (restaurant.Id != 0) return null;

            await _repository.Add(restaurant);
            return restaurant;
        }

        public async Task<Restaurant?> PutRestaurant(int id, Restaurant restaurant)
        {
            var existingRestaurant = await _repository.GetById(id);
            //Todo checks and returns of correct type for status codes
            if (existingRestaurant == null || id != restaurant.Id) return null;

            await _repository.Update(restaurant);
            return restaurant;
        }

        public async Task<bool> DeleteRestaurant(int id)
        {
            var existingRestaurant = await _repository.GetById(id);
            //    //Todo checks and returns of correct type for status codes
            if (existingRestaurant == null) return false;

            await _repository.Delete(existingRestaurant);
            return true;
        }
    }
}
