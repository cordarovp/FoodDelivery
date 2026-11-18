using FoodDelivery.DTO.Food;
using FoodDelivery.Result;

namespace FoodDelivery.Service.Interfaces
{
    public interface IFoodService
    {
        Task<IEnumerable<ResponseFoodDTO>> GetFoodList();
        Task<Reply<ResponseFoodDTO>> GetFoodById(int id);
        Task<Reply<ResponseFoodDTO>> PostFood(RequestFoodDTO food);
        Task<Reply> PutFood(int id, RequestFoodDTO food);
        Task<Reply> DeleteFood(int id);
    }
}
