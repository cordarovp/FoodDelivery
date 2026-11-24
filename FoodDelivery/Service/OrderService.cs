using FoodDelivery.DTO.Order;
using FoodDelivery.Entity;
using FoodDelivery.Repository.Interfaces;
using FoodDelivery.Result;
using FoodDelivery.Result.Error;
using FoodDelivery.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FoodDelivery.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<int, Food> _foodRepository;
        private readonly IRestaurantRepository _restaurantsRepository;

        public OrderService(IOrderRepository repository,
           IUserRepository userRepository,
           IRepository<int, Food> foodRepository,
           IRestaurantRepository restaurantRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _foodRepository = foodRepository;
            _restaurantsRepository = restaurantRepository;
        }

        public async Task<IEnumerable<ResponseOrderDTO>> GetOrderList()
        {
            var orderList = await _repository.GetAllWithDetails();

            var orderDTOList = new List<ResponseOrderDTO>();
            foreach (var order in orderList)
            {
                var orderDto = CreateOrderDTO(order);
                orderDTOList.Add(orderDto);
            }

            return orderDTOList;
        }

        public async Task<Reply<ResponseOrderDTO>> GetOrderById(int id)
        {
            var order = await _repository.GetByIdWithDetails(id);
            if (order == null) return Reply<ResponseOrderDTO>.Failure(ErrorCore.NotFound($"Order with id: '{id}' was not found!"));
            var orderDto = CreateOrderDTO(order);
            return Reply<ResponseOrderDTO>.Success(orderDto);
        }

        public async Task<Reply<ResponseOrderDTO>> PostOrder(RequestOrderDTO requestOrder)
        {
            var foundUser = await _userRepository.GetById(requestOrder.UserId);
            if (foundUser == null) return Reply<ResponseOrderDTO>.Failure(ErrorCore.UnprocessableContent($"User with id: '{requestOrder.UserId}' does not exist!"));

            var foundFood = await _foodRepository.GetById(requestOrder.FoodId);
            if (foundFood == null) return Reply<ResponseOrderDTO>.Failure(ErrorCore.UnprocessableContent($"Food with id: '{requestOrder.FoodId}' does not exist!"));

            var foundRestaurant = await FindNearestAvailableRestaurantAsync(foundUser.GeoLat, foundUser.GeoLon);
            if (foundRestaurant == null) return Reply<ResponseOrderDTO>.Failure(ErrorCore.Conflict($"No available Restaurants found for this order, please try again later!"));

            var order = MappingNewOrder(foundUser.Id, foundFood.Id, foundRestaurant.Id);
            try
            {
                await _repository.Add(order);
            }
            catch(DbUpdateConcurrencyException)
            {
                return Reply<ResponseOrderDTO>.Failure(ErrorCore.Conflict("Restaurant has already been used!"));
            }

            var fullOrder = await _repository.GetByIdWithDetails(order.Id);
            if (fullOrder == null) return Reply<ResponseOrderDTO>.Failure(ErrorCore.NotFound($"The Order with details by id: '{order.Id}' could not be found!"));
            var orderDto = CreateOrderDTO(fullOrder);
            return Reply<ResponseOrderDTO>.Success(orderDto);
        }

        //Should updating orders be available?
        //public async Task<Reply> PutOrder(int id, RequestOrderDTO order)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<Reply> DeleteOrder(int id)
        {
            var existingOrder = await _repository.GetById(id);
            if (existingOrder == null) return Reply.Failure(ErrorCore.NotFound($"Order with id: '{id}' was not found!"));

            try
            {
                await _repository.Delete(existingOrder);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Reply.Failure(ErrorCore.NotFound($"Order with id: '{id}' was not found (Id was probably changed/deleted)."));
            }
            return Reply.Success();
        }

        private ResponseOrderDTO CreateOrderDTO(Order existingOrder)
        {
            var orderDto = new ResponseOrderDTO
            {
                OrderId = existingOrder.Id,
                UserName = existingOrder.User.FirstName + " " + existingOrder.User.LastName,
                FoodName = existingOrder.Food.Name,
                RestaurantId = existingOrder.RestaurantId
            };

            return orderDto;
        }

        private Order MappingNewOrder(int userId, int foodId, int restaurantId)
        {
            Order order = new Order
            {
                Id = default,
                CreatedAt = DateTime.UtcNow,
                UserId = userId,
                FoodId = foodId,
                RestaurantId = restaurantId
            };
            return order;
        }
        private async Task<Restaurant?> FindNearestAvailableRestaurantAsync(double latitude, double longitude)
        {
            const double degToRad = Math.PI / 180;
            double cosUserLat = Math.Cos(latitude * degToRad);
            var availableRestaurants = await _restaurantsRepository.GetAllAvailableRestaurants(DateTime.UtcNow);
            if (availableRestaurants.IsNullOrEmpty()) return null;

            Restaurant? nearest = null;
            double minA = double.MaxValue;

            foreach (var restaurant in availableRestaurants)
            {
                double dLat = (restaurant!.GeoLat - latitude) * degToRad;
                double dLon = (restaurant!.GeoLon - longitude) * degToRad;

                double sinHalfLat = Math.Sin(dLat * 0.5);
                double sinHalfLon = Math.Sin(dLon * 0.5);

                double a = (sinHalfLat * sinHalfLat) + (cosUserLat * Math.Cos(restaurant.GeoLat * degToRad) * sinHalfLon * sinHalfLon);

                if (a < minA)
                {
                    minA = a;
                    nearest = restaurant;
                }
            }
            if(nearest != null) nearest.LatestOrderTime = DateTime.UtcNow;
            return nearest;
        }
    }
}
