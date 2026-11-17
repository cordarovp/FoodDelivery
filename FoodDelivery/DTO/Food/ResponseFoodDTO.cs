namespace FoodDelivery.DTO.Food
{
    public class ResponseFoodDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required decimal Price { get; set; }
    }
}
