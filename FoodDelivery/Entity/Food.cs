using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Entity
{
    public class Food
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Food must have a name!")]
        [MaxLength(100)]
        public required string Name { get; set; }
        [Required(ErrorMessage = "Food must have a set price!")]
        [Precision(14, 2)]
        public decimal Price { get; set; }
    }
}
