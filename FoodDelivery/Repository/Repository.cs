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

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }
        public async Task<TEntity?> GetById(int id)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(t => t.Id.Equals(id));
        }
        public async Task<TEntity> Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await Save();
            return entity;
        }
        public async Task Update(TEntity entity)
        {
            _dbSet.Update(entity);
            await Save();
        }
        public async Task Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            await Save();
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
