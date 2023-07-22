using CommerceClone.Models;
using Microsoft.EntityFrameworkCore;

namespace CommerceClone.Interfaces
{
    public interface IDataContext
    {
        DbSet<Item> Items { get; set; }
        DbSet<Store> Stores { get; set; }
        DbSet<Admin> Admins { get; set; }
        DbSet<Cart> Carts { get; set; }

        int SaveChanges();
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
