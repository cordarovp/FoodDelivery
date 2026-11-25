using Microsoft.EntityFrameworkCore;
using FoodDelivery.Entity;

namespace FoodDelivery.Data
{
    public class FoodDeliveryDbContext : DbContext
    {
        public FoodDeliveryDbContext(DbContextOptions<FoodDeliveryDbContext> options) : base(options)
        { }

        public DbSet<Food> Foods { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Restaurant>().HasQueryFilter(r => !r.IsDeleted);
            modelBuilder.Entity<Food>().HasQueryFilter(f => !f.IsDeleted);
            modelBuilder.Entity<Order>().HasQueryFilter(o => !o.IsDeleted);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Restaurant)
                .WithMany(r => r.Orders)
                .HasForeignKey(o => o.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Food)
                .WithMany()
                .HasForeignKey(o => o.FoodId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
