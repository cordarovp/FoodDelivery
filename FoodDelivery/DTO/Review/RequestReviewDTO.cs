using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.DTO.Review
{
    public class RequestReviewDTO
    {
        public required int UserId { get; set; }
        public required int FoodId { get; set; }
        [Range(1, 5, ErrorMessage = "Must set a rating for the reviewed food!")]
        public required int Rating { get; set; }
        [MaxLength(300, ErrorMessage = "Review comment cannot excede 300 characters!")]
        public string? Comment { get; set; }
    }
}
