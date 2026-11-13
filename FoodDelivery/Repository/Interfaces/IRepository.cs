using FoodDelivery.Entity;

namespace FoodDelivery.Repository.Interfaces
{
    public interface IRepository<TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity?> GetById(TKey id);
        Task<TEntity> Add(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
        Task Save();
    }
}
