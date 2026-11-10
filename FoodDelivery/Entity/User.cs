using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Entity
{
    [Index(nameof(Username), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }
        [MaxLength(60)]
        public required string FirstName { get; set; }
        [MaxLength(60)]
        public required string LastName { get; set; }
        [Required(ErrorMessage ="Must have latitude for delivery!")]
        public double GeoLat { get; set; }
        [Required(ErrorMessage = "Must have longitude for delivery!")]
        public double GeoLon { get; set; }
        [Required(ErrorMessage = "Must have a valid username!")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Must be between 5 and 30 characters")]
        public required string Username { get; set; }
        [Required(ErrorMessage = "Must have a valid password!")]
        public required string Password { get; set; }
        [Required(ErrorMessage = "Must be set for all users!")]
        public bool IsAdmin { get; set; }
    }
}
