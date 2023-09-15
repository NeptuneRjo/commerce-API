using AutoMapper;
using CommerceApi.BLL.Utilities;
using System.Linq.Expressions;

namespace CommerceApi.BLL.Services
{
    public class GenericService<TEntity> : IGenericService<TEntity>
    {
        private IMapper _mapper;
        private IGenericOperations<TEntity> _ops;

        public GenericService(IMapper mapper, IGenericOperations<TEntity> ops)
        {
            _mapper = mapper;
            _ops = ops;
        }

        public async Task<ICollection<TEntity>> GetEntitiesAsync() => 
            await _ops.RetrieveEntitiesOperation();

        public async Task<ICollection<TDestination>> GetEntitiesAsync<TDestination>() => 
            _mapper.Map<ICollection<TDestination>>(await _ops.RetrieveEntitiesOperation());

        public async Task<TEntity> GetEntityAsync(
            Expression<Func<TEntity, bool>> filter, 
            Expression<Func<TEntity, object>>[]? includes = null
            ) => await _ops.RetrieveEntityOperation(filter, includes);

        public async Task<TDestination> GetEntityAsync<TDestination>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, object>>[]? includes = null
            ) => _mapper.Map<TDestination>(await _ops.RetrieveEntityOperation(filter, includes));

        public async Task DeleteEntityAsync(Expression<Func<TEntity, bool>> filter) =>
            await _ops.DeleteEntityOperation(filter);

        public async Task<TEntity> AddEntityAsync(TEntity entity) =>
            await _ops.AddEntityOperation(entity);

        public async Task<TDestination> AddEntityAsync<TDestination>(TEntity entity) =>
            _mapper.Map<TDestination>(await _ops.AddEntityOperation(entity));

        public async Task<TEntity> UpdateEntityAsync(TEntity entity) =>
            await _ops.UpdateEntityOperation(entity);

        public async Task<TDestination> UpdateEntityAsync<TDestination>(TEntity entity) =>
            _mapper.Map<TDestination>(await _ops.UpdateEntityOperation(entity));

    }
}
