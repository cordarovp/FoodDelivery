using FoodDelivery.Entity;

namespace FoodDelivery.Repository.Interfaces
{
    public interface IRepository<in TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity?> GetById(int id);
        Task<TEntity> Add(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
        Task Save();
    }
}
