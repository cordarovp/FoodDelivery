using FoodDelivery.DTO.Review;
using FoodDelivery.Result;

namespace FoodDelivery.Service.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ResponseReviewDTO>> GetReviewList();
        Task<Reply<ResponseReviewDTO>> GetReviewById(int id);
        Task<Reply<ResponseReviewDTO>> PostReview(RequestReviewDTO order);
        Task<Reply> PutReview(int id, RequestReviewDTO order);
        Task<Reply> DeleteReview(int id);
    }
}
