using FoodDelivery.DTO.Food;
using FoodDelivery.Entity;
using FoodDelivery.Reply;
using FoodDelivery.Repository.Interfaces;
using FoodDelivery.Service.Interfaces;

namespace FoodDelivery.Service
{
    public class FoodService : IFoodService
    {
        private readonly IRepository<int, Food> _repository;

        public FoodService(IRepository<int, Food> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ResponseFoodDTO>> GetFoodList()
        {
            var foodList =  await _repository.GetAll();

            var foodDTOList = new List<ResponseFoodDTO>();
            foreach (var food in foodList)
            {
                var foodDto = CreateFoodDTO(food);
                foodDTOList.Add(foodDto);
            }

            return foodDTOList;
        }

        public async Task<Reply<ResponseFoodDTO>> GetFoodById(int id)
        {
            var food = await _repository.GetById(id);
            if (food == null) return Reply<ResponseFoodDTO>.NotFoundError($"Food with id: '{id}' was not found!");
            var foodDto = CreateFoodDTO(food);
            return Reply<ResponseFoodDTO>.Success(foodDto);
        }

        public async Task<Reply<ResponseFoodDTO>> PostFood(RequestFoodDTO requestFood)
        {
            if (requestFood == null) return Reply<ResponseFoodDTO>.ValidationError("Food was not forwarded!");
            if (requestFood.Price <= 0) return Reply<ResponseFoodDTO>.ValidationError("Cannot set Food price to a negative number!");

            var food = MappingFood(0, requestFood);
            await _repository.Add(food);

            var foodDto = CreateFoodDTO(food);
            return Reply<ResponseFoodDTO>.Success(foodDto);
        }

        public async Task<Reply<bool>> PutFood(int id, RequestFoodDTO requestFood)
        {
            var existingFood = await _repository.GetById(id);

            if (existingFood == null) return Reply<bool>.NotFoundError($"Food with id: {id} was not found!");
            if (requestFood.Price <= 0) return Reply<bool>.ValidationError("Cannot set Food price to a negative number!");

            var food = MappingFood(id, requestFood);
            await _repository.Update(food);
            return Reply<bool>.Success(true);
        }

        public async Task<Reply<bool>> DeleteFood(int id)
        {
            var existingFood = await _repository.GetById(id);
            if (existingFood == null) return Reply<bool>.NotFoundError($"Food with id: {id} was not found!");

            await _repository.Delete(existingFood);
            return Reply<bool>.Success(true);
        }

        private ResponseFoodDTO CreateFoodDTO(Food existingFood)
        {
            var foodDto = new ResponseFoodDTO
            {
                Id = existingFood.Id,
                Name = existingFood.Name,
                Price = existingFood.Price
            };

            return foodDto;
        }

        private Food MappingFood(int id, RequestFoodDTO requestFood)
        {
            Food food = new Food
            {
                Id = id,
                Name = requestFood.Name,
                Price = requestFood.Price
            };
            return food;
        }
    }
}
