namespace FoodDelivery.DTO.Review
{
    public class ResponseReviewDTO
    {
        public int ReviewId { get; set; }
        public required string FullName { get; set; }
        public required string FoodName { get; set; }
        public string? Comment { get; set; }
    }
}
