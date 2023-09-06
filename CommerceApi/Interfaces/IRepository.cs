using CommerceClone.Models;
using System.Linq.Expressions;

namespace CommerceClone.Interfaces
{
    public interface IRepository<TEntity>
    {
        TEntity Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        ICollection<TEntity> GetAll();
        /// <summary>
        /// Query the database. Include any reference objects.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includes"></param>
        /// <returns>The found entity or null</returns>
        TEntity GetByQuery(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        ICollection<TEntity> GetAllByQuery(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        void Update(int id, TEntity entity);
        void Delete(int id);
        void SaveChanges();
        /// <summary>
        /// Checks if the provided public or secret key matches the admin
        /// </summary>
        /// <param name="key"></param>
        /// <param name="admin"></param>
        /// <returns>true if there is a match</returns>
        bool PublicAuth(string key, Admin admin);
        /// <summary>
        /// Checks if the provided secret key matches the admin;
        /// </summary>
        /// <param name="key"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        bool PrivateAuth(string key, Admin admin);
    }
}
