namespace FoodDelivery.Entity
{
    public class Order : IEntity<int>
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; } = null!;
        public int FoodId { get; set; }
        public Food Food { get; set; } = null!;
    }
}
