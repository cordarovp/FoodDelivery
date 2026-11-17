using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.DTO.Restaurant
{
    public class ResponseRestaurantDTO
    {
        public int Id { get; set; }
        public required double GeoLat { get; set; }
        public required double GeoLon { get; set; }
    }
}
