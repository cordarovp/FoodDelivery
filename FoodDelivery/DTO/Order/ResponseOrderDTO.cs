namespace FoodDelivery.DTO.Order
{
    public class ResponseOrderDTO
    {
        public int orderId { get; set; }
        public required string userName { get; set; }
        public required string foodName { get; set; }
        public required int restaurantId { get; set; }
    }
}
