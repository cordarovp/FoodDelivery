using FoodDelivery.Entity.Interfaces;

namespace FoodDelivery.Entity
{
    public enum OrderStatus 
    { 
        Cancelled,
        Created,
        Delivering,
        Delivered
    }
    public class Order : IEntity<int>, ISoftDeletable
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public OrderStatus Status { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int? RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; } = null!;
        public int FoodId { get; set; }
        public Food Food { get; set; } = null!;

        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
