using CommerceClone.Models;
using Microsoft.EntityFrameworkCore;

namespace CommerceClone.Interfaces
{
    public interface IDataContext
    {
        DbSet<Product> Products { get; set; }
        DbSet<Cart> Carts { get; set; }
        DbSet<ProductReview> ProductReviews { get; set; }
        DbSet<CartProduct> CartProducts { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Review> Reviews { get; set; }
        DbSet<WishList> WishLists { get; set; }
        DbSet<WishListProduct> WishListProducts { get; set; }


        int SaveChanges();
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
