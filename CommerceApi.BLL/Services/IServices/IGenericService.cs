using System.Linq.Expressions;

namespace CommerceApi.BLL.Services
{
    public interface IGenericService<TEntity>
    {
        Task<IGenericService<TEntity>> GetEntitiesAsync();
        Task<IGenericService<TEntity>> GetEntityAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>>[] includes);
        Task<IGenericService<TEntity>> AddEntityAsync(TEntity entity);
        Task<IGenericService<TEntity>> AddEntitiesAsync(IList<TEntity> entities);
        Task<IGenericService<TEntity>> UpdateEntityAsync(TEntity entity);
        Task DeleteEntityAsync(TEntity entity);

        IGenericService<TEntity> MapEntity<TSource, TDestination>(TSource source);
        IGenericService<TEntity> MapEntities<TSource, TDestination>(IList<TSource> sourceList);
    }
}
