using CommerceClone.Interfaces;
using CommerceClone.Models;
using Microsoft.EntityFrameworkCore;

namespace CommerceClone.Data
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext() { }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // StoreItems
            modelBuilder.Entity<Store>()
                .HasMany(e => e.Items)
                .WithOne(e => e.Store)
                .HasForeignKey(e => e.StoreId);

            // CartItems
            modelBuilder.Entity<Cart>()
                .HasMany(e => e.Items)
                .WithOne(e => e.Cart)
                .HasForeignKey(e => e.CartId);
        }

    }
}
