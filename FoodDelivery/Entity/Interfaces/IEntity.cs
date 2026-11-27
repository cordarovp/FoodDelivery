namespace FoodDelivery.Entity.Interfaces
{
    public interface IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
    }
}
