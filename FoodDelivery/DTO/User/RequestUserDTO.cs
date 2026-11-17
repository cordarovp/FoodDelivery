using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.DTO.User
{
    public class RequestUserDTO
    {
        [MaxLength(60)]
        public required string FirstName { get; set; }

        [MaxLength(60)]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Must have a valid username!")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Must be between 5 and 30 characters")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Must have a valid password!")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Must have latitude for delivery!")]
        public required double GeoLat { get; set; }

        [Required(ErrorMessage = "Must have longitude for delivery!")]
        public required double GeoLon { get; set; }
    }
}