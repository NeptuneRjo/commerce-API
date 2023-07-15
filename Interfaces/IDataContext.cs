using CommerceClone.Models;
using Microsoft.EntityFrameworkCore;

namespace CommerceClone.Interfaces
{
    public interface IDataContext
    {
        DbSet<Admin> Admins { get; set; }
        DbSet<Cart> Carts { get; set; }
        DbSet<Item> Items { get; set; }
        DbSet<Store> Stores { get; set; }
        DbSet<User> Users { get; set; }

        int SaveChanges();
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
