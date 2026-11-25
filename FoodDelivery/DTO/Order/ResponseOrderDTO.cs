namespace FoodDelivery.DTO.Order
{
    public class ResponseOrderDTO
    {
        public int OrderId { get; set; }
        public required string UserName { get; set; }
        public required string FoodName { get; set; }
        public required int? RestaurantId { get; set; }
        public required string OrderStatus { get; set; }
    }
}
