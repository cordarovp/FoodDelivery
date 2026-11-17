using FoodDelivery.DTO.Food;
using FoodDelivery.Entity;
using FoodDelivery.Reply;

namespace FoodDelivery.Service.Interfaces
{
    public interface IFoodService
    {
        Task<IEnumerable<ResponseFoodDTO>> GetFoodList();
        Task<Reply<ResponseFoodDTO>> GetFoodById(int id);
        Task<Reply<ResponseFoodDTO>> PostFood(RequestFoodDTO food);
        Task<Reply<bool>> PutFood(int id, RequestFoodDTO food);
        Task<Reply<bool>> DeleteFood(int id);
    }
}
