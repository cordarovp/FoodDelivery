using FoodDelivery.Data;
using FoodDelivery.Entity;
using FoodDelivery.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Repository
{
    public class Repository<TKey, TEntity> : IRepository<TKey, TEntity>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly FoodDeliveryDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(FoodDeliveryDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsNoTracking().ToList();
        }
        public TEntity? GetById(int id)
        {
            return _dbSet.AsNoTracking().FirstOrDefault(t => t.Id.Equals(id));
        }
        public void Add(TEntity entetiy)
        {
            _dbSet.Add(entetiy);
            Save();
        }
        public void Update(TEntity entetiy)
        {
            _dbSet.Update(entetiy);
            Save();
        }
        public void Delete(TEntity entetiy)
        {
            _dbSet.Remove(entetiy);
            Save();
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
