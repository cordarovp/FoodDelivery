using FoodDelivery.Entity;

namespace FoodDelivery.Repository.Interfaces
{
    public interface IRepository<in TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IEntity<TKey>
    {
        IEnumerable<TEntity> GetAll();
        TEntity? GetById(int id);
        void Add(TEntity entetiy);
        void Update(TEntity entetiy);
        void Delete(TEntity entetiy);
        void Save();
    }
}
