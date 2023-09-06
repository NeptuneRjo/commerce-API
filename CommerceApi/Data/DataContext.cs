using CommerceApi.Interfaces;
using CommerceApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CommerceClone.Data
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext() { }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductReview> ProductReviews { get; set; }

        public DbSet<CartProduct> CartProducts { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<WishList> WishLists { get; set; }

        public DbSet<WishListProduct> WishListProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ProductReview Join-Table
            modelBuilder.Entity<ProductReview>()
                .HasKey(pr => new { pr.ProductId, pr.ReviewId });

            modelBuilder.Entity<ProductReview>()
                .HasOne(pr => pr.Product)
                .WithMany(pr => pr.ProductReviews)
                .HasForeignKey(pr => pr.ProductId);

            modelBuilder.Entity<ProductReview>()
                .HasOne(pr => pr.Review)
                .WithMany(pr => pr.ProductReviews)
                .HasForeignKey(pr => pr.ReviewId);

            // CartProduct Join-Table
            modelBuilder.Entity<CartProduct>()
                .HasKey(cp => new { cp.CartId, cp.ProductId });

            modelBuilder.Entity<CartProduct>()
                .HasOne(cp => cp.Product)
                .WithMany(cp => cp.CartProducts)
                .HasForeignKey(cp => cp.ProductId);

            modelBuilder.Entity<CartProduct>()
                .HasOne(cp => cp.Cart)
                .WithMany(cp => cp.CartProducts)
                .HasForeignKey(cp => cp.CartId);

            // WishListProduct Join-Table
            modelBuilder.Entity<WishListProduct>()
                .HasKey(wp => new { wp.ProductId, wp.WishListId });

            modelBuilder.Entity<WishListProduct>()
                .HasOne(wp => wp.Product)
                .WithMany(wp => wp.WishListProducts)
                .HasForeignKey(wp => wp.ProductId);

            modelBuilder.Entity<WishListProduct>()
                .HasOne(wp => wp.WishList)
                .WithMany(wp => wp.WishListProducts)
                .HasForeignKey(wp => wp.WishListId);

            // User ~ Cart Relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.Cart)
                .WithOne(u => u.User)
                .HasForeignKey<Cart>(u => u.UserId);
        
            // User ~ WishList Relationship
            modelBuilder.Entity<User>()
                .HasOne(u => u.WishList)
                .WithOne(u => u.User)
                .HasForeignKey<WishList>(u => u.UserId);
        }
    }
}
