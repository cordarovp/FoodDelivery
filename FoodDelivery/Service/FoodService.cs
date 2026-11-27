using FoodDelivery.DTO.Food;
using FoodDelivery.DTO.Review;
using FoodDelivery.Entity;
using FoodDelivery.Repository.Interfaces;
using FoodDelivery.Result;
using FoodDelivery.Result.Error;
using FoodDelivery.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Service
{
    public class FoodService : IFoodService
    {
        private readonly IFoodRepository _repository;

        public FoodService(IFoodRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ResponseFoodDTO>> GetFoodList()
        {
            var foodList = await _repository.GetAllWithReview();

            var foodDTOList = new List<ResponseFoodDTO>();
            foreach (var food in foodList)
            {
                var foodDto = CreateFoodDTO(food);
                foodDTOList.Add(foodDto);
            }

            return foodDTOList.OrderByDescending(dto => dto.AverageRating);
        }

        public async Task<Reply<ResponseFoodDTO>> GetFoodById(int id)
        {
            var food = await _repository.GetByIdWithReview(id);
            if (food == null) return Reply<ResponseFoodDTO>.Failure(ErrorCore.NotFound($"Food with id: '{id}' was not found!"));
            var foodDto = CreateFoodDTO(food);
            return Reply<ResponseFoodDTO>.Success(foodDto);
        }

        public async Task<Reply<ResponseFoodDTO>> PostFood(RequestFoodDTO requestFood)
        {
            if (requestFood == null) return Reply<ResponseFoodDTO>.Failure(ErrorCore.Validation("Food was not forwarded!"));
            if (requestFood.Price <= 0) return Reply<ResponseFoodDTO>.Failure(ErrorCore.Validation("Cannot set Food price to zero or negative number!"));

            var food = MappingNewFood(requestFood);
            await _repository.Add(food);

            var fullFood = _repository.GetByIdWithReview(food.Id);
            if (fullFood == null) return Reply<ResponseFoodDTO>.Failure(ErrorCore.NotFound($"Food with details by id: '{food.Id}' could not be found!"));
            var foodDto = CreateFoodDTO(food);
            return Reply<ResponseFoodDTO>.Success(foodDto);
        }

        public async Task<Reply> PutFood(int id, RequestFoodDTO requestFood)
        {
            var existingFood = await _repository.GetById(id);

            if (existingFood == null) return Reply.Failure(ErrorCore.NotFound($"Food with id: '{id}' was not found!"));
            if (requestFood.Price <= 0) return Reply.Failure(ErrorCore.Validation("Cannot set Food price to a negative number or negative number!"));

            MappingFood(existingFood, requestFood);
            try
            {
                await _repository.Update(existingFood);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Reply.Failure(ErrorCore.NotFound($"Food with id: {id} was not found (Id was probably changed/deleted)."));
            }
            return Reply.Success();
        }

        public async Task<Reply> DeleteFood(int id)
        {
            var existingFood = await _repository.GetById(id);
            if (existingFood == null) return Reply.Failure(ErrorCore.NotFound($"Food with id: '{id}' was not found!"));

            try
            {
                await _repository.Delete(existingFood);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Reply.Failure(ErrorCore.NotFound($"Food with id: {id} was not found (Id was probably changed/deleted)."));
            }
            return Reply.Success();
        }

        private ResponseFoodDTO CreateFoodDTO(Food existingFood)
        {
            var foodDto = new ResponseFoodDTO
            {
                Id = existingFood.Id,
                Name = existingFood.Name,
                Price = existingFood.Price,
                AverageRating = CalculateAverageRating(existingFood.Reviews),
                FirstComment = existingFood.Reviews.FirstOrDefault()?.Comment,
            };

            return foodDto;
        }

        private Food MappingNewFood(RequestFoodDTO requestFood)
        {
            Food food = new Food
            {
                Name = requestFood.Name,
                Price = requestFood.Price
            };
            return food;
        }

        private void MappingFood(Food existingFood, RequestFoodDTO requestFood)
        {
            existingFood.Name = requestFood.Name;
            existingFood.Price = requestFood.Price;
        }

        private double? CalculateAverageRating(ICollection<Review> reviewList)
        {
            if (!reviewList.Any() && reviewList != null) return null;

            return reviewList?.Average(r => r.Rating);
        }
    }
}
