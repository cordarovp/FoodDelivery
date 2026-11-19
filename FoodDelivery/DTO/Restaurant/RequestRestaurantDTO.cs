using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.DTO.Restaurant
{
    public class RequestRestaurantDTO
    {
        [Range(-90.0, 90.0, ErrorMessage = "Latitude must be set between the range of -90 and 90.")]
        public required double GeoLat { get; set; }

        [Range(-180.0, 180.0, ErrorMessage = "Longitude must be set between the range of -180 and 180.")]
        public required double GeoLon { get; set; }
    }
}
