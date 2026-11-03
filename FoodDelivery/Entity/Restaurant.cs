using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Entity
{
    public class Restaurant
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Requires location of Restaurant!")]
        public string Geolocation { get; set; }
    }
}
