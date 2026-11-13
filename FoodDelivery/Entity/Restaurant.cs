using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Entity
{
    public class Restaurant : IEntity<int>
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Requires latidude of Restaurant!")]
        public double GeoLat { get; set; }
        [Required(ErrorMessage = "Requires longitude of Restaurant!")]
        public double GeoLon { get; set; }
    }
}
