using AutoMapper;
using CommerceApi.DAL.Repositories;
using System.Linq.Expressions;

namespace CommerceApi.BLL.Services
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
    {
        private TEntity _entity;
        private IList<TEntity> _entities;

        private readonly IGenericRepository<TEntity> _repository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IGenericService<TEntity>> GetEntitiesAsync()
        {
            _entities = (IList<TEntity>)await _repository.GetAll();

            return this;
        }

        public async Task<IGenericService<TEntity>> GetEntityAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>>[] includes)
        {
            _entity = await _repository.GetByQuery(filter, includes);

            return this;
        }

        public async Task<IGenericService<TEntity>> AddEntityAsync(TEntity entity)
        {
            _entity = await _repository.Add(entity);

            return this;
        }

        public async Task<IGenericService<TEntity>> AddEntitiesAsync(IList<TEntity> entities)
        {
            _entities = (IList<TEntity>)await _repository.AddRange(entities);

            return this;
        }

        public async Task<IGenericService<TEntity>> UpdateEntityAsync(TEntity entity)
        {
            _entity = await _repository.Update(entity);

            return this;
        }

        public async Task DeleteEntityAsync(TEntity entity)
        {
            await _repository.Delete(entity);
        }

        public IGenericService<TEntity> MapEntities<TSource, TDestination>(IList<TSource> sourceList)
        {
            _entities = _mapper.Map<IList<TEntity>>(sourceList);

            return this;
        }

        public IGenericService<TEntity> MapEntity<TSource, TDestination>(TSource source)
        {
            _entity = _mapper.Map<TEntity>(source);

            return this;
        }
    }
}
