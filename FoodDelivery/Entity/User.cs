using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.Entity
{
    [Index(nameof(Username), IsUnique = true)]
    public class User
    {
        public int Id { get; set; }
        [MaxLength(60)]
        public string FirstName { get; set; }
        [MaxLength(60)]
        public string LastName { get; set; }
        [Required(ErrorMessage ="Must have location for delivery!")]
        public string Geolocation { get; set; }
        [Required(ErrorMessage = "Must have a valid username!")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Must be between 5 and 30 characters")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Must have a valid password!")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Must be set for all users!")]
        public bool IsAdmin { get; set; }
    }
}
