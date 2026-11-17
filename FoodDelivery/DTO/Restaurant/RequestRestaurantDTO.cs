using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.DTO.Restaurant
{
    public class RequestRestaurantDTO
    {
        [Required(ErrorMessage = "Requires latidude of Restaurant!")]
        public required double GeoLat { get; set; }
        [Required(ErrorMessage = "Requires longitude of Restaurant!")]
        public required double GeoLon { get; set; }
    }
}
