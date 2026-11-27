using FoodDelivery.Entity.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Entity
{
    public class Review : IEntity<int>, ISoftDeletable
    {
        public int Id { get; set; }
        [Required]
        [Range(1, 5, ErrorMessage = "Must set a rating for the reviewed food!")]
        public int Rating { get; set; }
        [MaxLength(300, ErrorMessage = "Review comment cannot excede 300 characters!")]
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int FoodId { get; set; }
        public Food Food { get; set; } = null!;

        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
