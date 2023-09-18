using System.Linq.Expressions;


namespace CommerceApi.BLL.Utilities
{
    public interface IGenericOperations<TEntity>
    {
        Task<TEntity> RetrieveEntityOperation(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>>[]? includes = null);

        Task<ICollection<TEntity>> RetrieveEntitiesOperation();

        Task<TEntity> UpdateEntityOperation(Expression<Func<TEntity, bool>> filter, TEntity entity);

        Task DeleteEntityOperation(Expression<Func<TEntity, bool>> filter);

        Task<TEntity> AddEntityOperation(TEntity entity);
    }
}
