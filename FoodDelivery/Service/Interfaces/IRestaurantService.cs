using FoodDelivery.DTO.Restaurant;
using FoodDelivery.Reply;

namespace FoodDelivery.Service.Interfaces
{
    public interface IRestaurantService
    {
        Task<IEnumerable<ResponseRestaurantDTO>> GetRestaurantList();
        Task<Reply<ResponseRestaurantDTO>> GetRestaurantById(int id);
        Task<Reply<ResponseRestaurantDTO>> PostRestaurant(RequestRestaurantDTO restaurant);
        Task<Reply<bool>> PutRestaurant(int id, RequestRestaurantDTO restaurant);
        Task<Reply<bool>> DeleteRestaurant(int id);
    }
}
