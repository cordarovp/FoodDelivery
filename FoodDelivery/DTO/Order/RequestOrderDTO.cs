namespace FoodDelivery.DTO.Order
{
    public class RequestOrderDTO
    {
        public required int UserId { get; set; }
        public required int FoodId { get; set; }
    }
}
