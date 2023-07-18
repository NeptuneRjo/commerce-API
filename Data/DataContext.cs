using CommerceClone.Interfaces;
using CommerceClone.Models;
using Microsoft.EntityFrameworkCore;

namespace CommerceClone.Data
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext() { }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Item> Items { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // StoreItems
            modelBuilder.Entity<Store>()
                .HasMany(e => e.Items)
                .WithOne(e => e.Store)
                .HasForeignKey(e => e.StoreId);

            // UserCart
            modelBuilder.Entity<User>()
                .HasMany(e => e.Cart) // Items
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);

            // AdminStores
            modelBuilder.Entity<Admin>()
                .HasMany(e => e.Stores)
                .WithOne(e => e.Admin)
                .HasForeignKey(e => e.AdminId);
        }

    }
}
