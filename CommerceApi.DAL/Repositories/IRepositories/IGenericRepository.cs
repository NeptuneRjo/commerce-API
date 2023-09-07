using System.Linq.Expressions;

namespace CommerceApi.DAL.Repositories
{
    public interface IGenericRepository<TEntity>
    {
        /// <summary>
        /// Add an entity to the database asynchronously
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Returns the added <see cref="TEntity"/></returns>
        Task<TEntity> Add(TEntity entity);

        /// <summary>
        /// Add a collection of entities to the database asynchronously
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>Returns the collection of added <see cref="ICollection{TEntity}"/></returns>
        Task<ICollection<TEntity>> AddRange(ICollection<TEntity> entities);

        /// <summary>
        /// Retrieves a collection of entities asynchronously
        /// </summary>
        /// <returns>The retrieved <see cref="ICollection{TEntity}"/></returns>
        Task<ICollection<TEntity>> GetAll();

        /// <summary>
        /// Retrieve an entity by a query asynchronously
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includes"></param>
        /// <returns>The retrieved <see cref="TEntity"/></returns>
        Task<TEntity> GetByQuery(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Retrieve a collection of entities by a query asynchronously
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includes"></param>
        /// <returns>The retrieved <see cref="ICollection{TEntity}"/></returns>
        Task<ICollection<TEntity>> GetAllByQuery(params Expression<Func<TEntity, object>>[] includes);
        
        /// <summary>
        /// Update an entity asynchronously
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns>The updated <see cref="TEntity"/></returns>
        Task<TEntity> Update(TEntity entity);

        /// <summary>
        /// Delete an entity asynchronously
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task Delete(TEntity entity);

        /// <summary>
        /// Save the context's changes asynchronously
        /// </summary>
        /// <returns></returns>
        Task SaveChanges();
    }
}
