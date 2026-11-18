using FoodDelivery.DTO.Restaurant;
using FoodDelivery.Entity;
using FoodDelivery.Reply;
using FoodDelivery.Repository.Interfaces;
using FoodDelivery.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Service
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRepository<int, Restaurant> _repository;

        public RestaurantService(IRepository<int, Restaurant> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ResponseRestaurantDTO>> GetRestaurantList()
        {
            var restaurantsList = await _repository.GetAll();

            var restaurantsDTOList = new List<ResponseRestaurantDTO>();
            foreach (var restaurant in restaurantsList)
            {
                var restaurantDto = CreateRestaurantDTO(restaurant);
                restaurantsDTOList.Add(restaurantDto);
            }

            return restaurantsDTOList;
        }

        public async Task<Reply<ResponseRestaurantDTO>> GetRestaurantById(int id)
        {
            var restaurant = await _repository.GetById(id);
            if (restaurant == null) return Reply<ResponseRestaurantDTO>.NotFoundError($"Restaurant with id: '{id}' was not found!");

            var restaurantDto = CreateRestaurantDTO(restaurant);
            return Reply<ResponseRestaurantDTO>.Success(restaurantDto);
        }

        public async Task<Reply<ResponseRestaurantDTO>> PostRestaurant(RequestRestaurantDTO requestRestaurant)
        {
            if (requestRestaurant == null) return Reply<ResponseRestaurantDTO>.NotFoundError($"Restaurant was not forwarded!");
            //Todo check for restaurant in the same position

            var restaurant = MappingRestaurant(0, requestRestaurant);
            await _repository.Add(restaurant);

            var restaurantDto = CreateRestaurantDTO(restaurant);
            return Reply<ResponseRestaurantDTO>.Success(restaurantDto);
        }

        public async Task<Reply<bool>> PutRestaurant(int id, RequestRestaurantDTO requestRestaurant)
        {
            var existingRestaurant = await _repository.GetById(id);
            if (existingRestaurant == null) return Reply<bool>.NotFoundError($"Restaurant by id: '{id}' was not forwarded!");
            //Todo check for restaurant in the same position

            var restaurant = MappingRestaurant(id, requestRestaurant);
            try
            {
                await _repository.Update(restaurant);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Reply<bool>.NotFoundError($"Restaurant with id: {id} was not found (Id was probably changed/deleted).");
            }
            return Reply<bool>.Success(true);
        }

        public async Task<Reply<bool>> DeleteRestaurant(int id)
        {
            var existingRestaurant = await _repository.GetById(id);
            if (existingRestaurant == null) return Reply<bool>.NotFoundError($"Restaurant by id: '{id}' was not forwarded!");

            try
            {
                await _repository.Delete(existingRestaurant);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Reply<bool>.NotFoundError($"Restaurant with id: {id} was not found (Id was probably changed/deleted).");
            }
            return Reply<bool>.Success(true);
        }

        private ResponseRestaurantDTO CreateRestaurantDTO(Restaurant restaurant)
        {
            var restaurantDto = new ResponseRestaurantDTO
            {
                Id = restaurant.Id,
                GeoLat = restaurant.GeoLat,
                GeoLon = restaurant.GeoLon
            };

            return restaurantDto;
        }

        private Restaurant MappingRestaurant(int id, RequestRestaurantDTO requestRestaurant)
        {
            Restaurant restaurant = new Restaurant
            {
                Id = id,
                GeoLat = requestRestaurant.GeoLat,
                GeoLon = requestRestaurant.GeoLon
            };

            return restaurant;
        }
    }
}
