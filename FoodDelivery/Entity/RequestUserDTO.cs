namespace FoodDelivery.Entity
{
    public class RequestUserDTO
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required double GeoLat { get; set; }
        public required double GeoLon { get; set; }
    }
}