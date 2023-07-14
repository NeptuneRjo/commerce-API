using CommerceClone.Interfaces;
using CommerceClone.Models;
using Microsoft.EntityFrameworkCore;

namespace CommerceClone.Data
{
    public class DataContext : DbContext, IDataContext
    {

        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Admin> Admins { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<Cart> Carts { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<Item> Items { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<Store> Stores { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DbSet<User> Users { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // AdminStores
            modelBuilder.Entity<Admin>()
                .HasMany(e => e.Stores)
                .WithOne(e => e.Admin)
                .HasForeignKey(e => e.AdminId);

            // StoreItems
            modelBuilder.Entity<Store>()
                .HasMany(e => e.Items)
                .WithOne(e => e.Store)
                .HasForeignKey(e => e.StoreId);

            // UserCart
            modelBuilder.Entity<User>()
                .HasOne(e => e.Cart)
                .WithOne(e => e.User)
                .HasForeignKey<Cart>(e => e.UserId);

            // CartItems
            modelBuilder.Entity<Cart>()
                .HasMany(e => e.Items)
                .WithOne(e => e.Cart)
                .HasForeignKey(e => e.CartId);
        }

    }
}
