namespace FoodDelivery.Entity
{
    public class UserDTO
    {
        public int Id {  get; set; }
        public required string FullName  { get; set; }
        public required string Username { get; set; }
        public required double GeoLat { get; set; }
        public required double GeoLon { get; set; }
    }
}
