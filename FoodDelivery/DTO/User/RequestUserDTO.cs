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

        [Range(-90.0, 90.0, ErrorMessage = "Latitude must be set between the range of -90 and 90.")]
        public required double GeoLat { get; set; }

        [Range(-180.0, 180.0, ErrorMessage = "Longitude must be set between the range of -180 and 180.")]
        public required double GeoLon { get; set; }
    }
}