using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.DTO.Food
{
    public class RequestFoodDTO
    {
        [Required(ErrorMessage = "Food must have a name!")]
        [MaxLength(100)]
        public required string Name { get; set; }
        public required decimal Price { get; set; }
    }
}
