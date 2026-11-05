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
            return await _repository.GetFoodListAsync();
        }

        public async Task<Food?> GetFoodById(int id)
        {
            return await _repository.GetFoodByIdAsync(id);
        }

        public async Task<Food> PostFood(Food food)
        {
            if (food.Price <= 0) throw new ArgumentException("Price is not set correctly!");

            await _repository.PostFoodAsync(food);
            await _repository.SaveChangesAsync();
            return food;
        }

        public async Task<bool> PutFoodAsync(int id, Food food)
        {
            var existingFood = await _repository.GetFoodByIdAsync(id);
            if (existingFood == null && id != food.Id)
            {
                return false;
            }

            _repository.PutFoodAsync(food);
            return await _repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteFoodAsync(int id)
        {
            var existingFood = await _repository.GetFoodByIdAsync(id);
            if(existingFood == null)
            {
                return false;
            }

            _repository.DeleteFoodAsync(existingFood);
            return await _repository.SaveChangesAsync();
        }
    }
}
