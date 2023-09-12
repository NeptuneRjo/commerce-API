using System.Linq.Expressions;


namespace CommerceApi.BLL.Services
{
    public interface IGenericService<TEntity>
    {
        Task<TEntity> GetEntityAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>>[]? includes);
        Task<TDestination> GetEntityAsync<TDestination>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>>[]? includes);

        Task<ICollection<TEntity>> GetEntitiesAsync();
        Task<ICollection<TDestination>> GetEntitiesAsync<TDestination>();

        Task DeleteEntityAsync(Expression<Func<TEntity, bool>> filter);
    }
}

