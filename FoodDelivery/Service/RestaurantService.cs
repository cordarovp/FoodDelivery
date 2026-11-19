using FoodDelivery.DTO.Restaurant;
using FoodDelivery.Entity;
using FoodDelivery.Repository.Interfaces;
using FoodDelivery.Result;
using FoodDelivery.Result.Error;
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
            if (restaurant == null) return Reply<ResponseRestaurantDTO>.Failure(ErrorCore.NotFound($"Restaurant with id: '{id}' was not found!"));

            var restaurantDto = CreateRestaurantDTO(restaurant);
            return Reply<ResponseRestaurantDTO>.Success(restaurantDto);
        }

        public async Task<Reply<ResponseRestaurantDTO>> PostRestaurant(RequestRestaurantDTO requestRestaurant)
        {
            if (requestRestaurant == null) return Reply<ResponseRestaurantDTO>.Failure(ErrorCore.Validation($"Restaurant was not forwarded!"));
            //Todo check for restaurant in the same position

            var restaurant = MappingNewRestaurant(0, requestRestaurant);
            await _repository.Add(restaurant);

            var restaurantDto = CreateRestaurantDTO(restaurant);
            return Reply<ResponseRestaurantDTO>.Success(restaurantDto);
        }

        public async Task<Reply> PutRestaurant(int id, RequestRestaurantDTO requestRestaurant)
        {
            var existingRestaurant = await _repository.GetById(id);
            if (existingRestaurant == null) return Reply.Failure(ErrorCore.NotFound($"Restaurant by id: '{id}' was not found!"));
            //Todo check for restaurant in the same position

            MappingRestaurant(existingRestaurant, requestRestaurant);
            try
            {
                await _repository.Update(existingRestaurant);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Reply.Failure(ErrorCore.NotFound($"Restaurant with id: {id} was not found (Id was probably changed/deleted)."));
            }
            return Reply.Success();
        }

        public async Task<Reply> DeleteRestaurant(int id)
        {
            var existingRestaurant = await _repository.GetById(id);
            if (existingRestaurant == null) return Reply.Failure(ErrorCore.NotFound($"Restaurant by id: '{id}' was not found!"));

            try
            {
                await _repository.Delete(existingRestaurant);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Reply.Failure(ErrorCore.NotFound($"Restaurant with id: {id} was not found (Id was probably changed/deleted)."));
            }
            return Reply.Success();
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

        private Restaurant MappingNewRestaurant(int id, RequestRestaurantDTO requestRestaurant)
        {
            Restaurant restaurant = new Restaurant
            {
                Id = id,
                GeoLat = requestRestaurant.GeoLat,
                GeoLon = requestRestaurant.GeoLon
            };

            return restaurant;
        }

        private void MappingRestaurant(Restaurant existingRestaurant, RequestRestaurantDTO requestRestaurant)
        {
            existingRestaurant.GeoLat = requestRestaurant.GeoLat;
            existingRestaurant.GeoLon = requestRestaurant.GeoLon;
        }
    }
}
