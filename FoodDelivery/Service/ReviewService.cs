using FoodDelivery.DTO.Order;
using FoodDelivery.DTO.Review;
using FoodDelivery.Entity;
using FoodDelivery.Repository.Interfaces;
using FoodDelivery.Result;
using FoodDelivery.Result.Error;
using FoodDelivery.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Service
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<int, Food> _foodRepository;

        public ReviewService(IReviewRepository repository, IUserRepository userRepository, IRepository<int, Food> foodRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _foodRepository = foodRepository;
        }

        public async Task<IEnumerable<ResponseReviewDTO>> GetReviewList()
        {
            var reviewList = await _repository.GetAllWithDetails();

            var reviewDTOList = new List<ResponseReviewDTO>();
            foreach (var review in reviewList)
            {
                var reviewDto = CreateReviewDTO(review);
                reviewDTOList.Add(reviewDto);
            }

            return reviewDTOList;
        }

        public async Task<Reply<ResponseReviewDTO>> GetReviewById(int id)
        {
            var review = await _repository.GetByIdWithDetails(id);
            if (review == null) return Reply<ResponseReviewDTO>.Failure(ErrorCore.NotFound($"Review with id: '{id}' was not found!"));
            var reviewDto = CreateReviewDTO(review);
            return Reply<ResponseReviewDTO>.Success(reviewDto);
        }

        public async Task<Reply<ResponseReviewDTO>> PostReview(RequestReviewDTO requestReview)
        {
            var foundUser = await _userRepository.GetById(requestReview.UserId);
            if (foundUser == null) return Reply<ResponseReviewDTO>.Failure(ErrorCore.UnprocessableContent($"User with id: '{requestReview.UserId}' does not exist!"));

            var foundFood = await _foodRepository.GetById(requestReview.FoodId);
            if (foundFood == null) return Reply<ResponseReviewDTO>.Failure(ErrorCore.UnprocessableContent($"Food with id: '{requestReview.FoodId}' does not exist!"));

            var review = MappingNewReview(requestReview.Rating, requestReview.Comment, foundUser.Id, foundFood.Id);
            await _repository.Add(review);

            var fullReview = await _repository.GetByIdWithDetails(review.Id);
            if(fullReview == null) return Reply<ResponseReviewDTO>.Failure(ErrorCore.NotFound($"The Review with details by id: '{review.Id}' could not be found!"));
            var reviewDto = CreateReviewDTO(fullReview);
            return Reply<ResponseReviewDTO>.Success(reviewDto);
        }

        public async Task<Reply> PutReview(int id, RequestReviewDTO requestReview)
        {
            var existingReview = await _repository.GetById(id);
            if (existingReview == null) return Reply.Failure(ErrorCore.NotFound($"Review with id: '{id}' was not found!"));

            MappingReview(existingReview, requestReview);
            try
            {
                await _repository.Update(existingReview);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Reply.Failure(ErrorCore.NotFound($"Review with id: {id} was not found (Id was probably changed/deleted)."));
            }
            return Reply.Success();
        }

        public async Task<Reply> DeleteReview(int id)
        {
            var existingReview = await _repository.GetById(id);
            if (existingReview == null) return Reply.Failure(ErrorCore.NotFound($"Review with id: '{id}' was not found"));

            try
            {
                await _repository.Delete(existingReview);
            }
            catch(DbUpdateConcurrencyException)
            {
                return Reply.Failure(ErrorCore.NotFound($"Review with id: '{id}' was not found (Id was probably changed/deleted)."));
            }
            return Reply.Success();
        }

        private ResponseReviewDTO CreateReviewDTO(Review existingReview)
        {
            var reviewDto = new ResponseReviewDTO
            {
                ReviewId = existingReview.Id,
                FullName = existingReview.User.FirstName + " " + existingReview.User.LastName,
                FoodName = existingReview.Food.Name,
                Comment = existingReview.Comment
            };
            return reviewDto;
        }

        private void MappingReview(Review existingReview, RequestReviewDTO requestReview)
        {
            existingReview.Rating = requestReview.Rating;
            existingReview.Comment = requestReview.Comment;
        }

        private Review MappingNewReview(int rating, string? comment, int userId, int foodId)
        {
            Review review = new Review
            {
                UserId = userId,
                FoodId = foodId,
                Rating = rating,
                Comment = comment
            };
            return review;
        }
    }
}
