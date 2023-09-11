using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
