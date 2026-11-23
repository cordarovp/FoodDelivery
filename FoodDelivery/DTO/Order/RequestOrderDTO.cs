namespace FoodDelivery.DTO.Order
{
    public class RequestOrderDTO
    {
        public required int userId { get; set; }
        public required int foodId { get; set; }
    }
}
