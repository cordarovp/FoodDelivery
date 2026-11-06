using FoodDelivery.Data;
using FoodDelivery.Entity;
using FoodDelivery.Repository;
using Microsoft.IdentityModel.Tokens;

namespace FoodDelivery.Service
{
    public class FoodService : IFoodService
    {
        private readonly IFoodRepository _repository;

        public FoodService(IFoodRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Food>> GetFoodList()
        {
            return await _repository.GetFoodList();
        }

        public async Task<Food?> GetFoodByIdAsync(int id)
        {
            return await _repository.GetFoodByIdAsync(id);
        }

        public async Task<Food?> PostFoodAsync(Food food)
        {
            if (food.Price <= 0) return null;

            await _repository.AddFoodAsync(food);
            await _repository.SaveChangesAsync();
            return food;
        }

        public async Task<bool> PutFood(int id, Food food)
        {
            var existingFood = await _repository.GetFoodByIdAsync(id);
            if (existingFood == null && id != food.Id)
            {
                return false;
            }

            _repository.UpdateFood(food);
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteFood(int id)
        {
            var existingFood = await _repository.GetFoodByIdAsync(id);
            if(existingFood == null)
            {
                return false;
            }

            _repository.RemoveFood(existingFood);
            return await _repository.SaveChangesAsync();
        }
    }
}
