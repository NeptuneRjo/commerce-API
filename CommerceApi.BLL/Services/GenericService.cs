using AutoMapper;
using CommerceApi.BLL.Utilities;
using CommerceApi.DAL.Repositories;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace CommerceApi.BLL.Services
{
    public class GenericService<TEntity> : IGenericService<TEntity>
    {
        private IGenericRepository<TEntity> _repository;
        private ILogger _logger;
        private IMapper _mapper;

        public GenericService(IGenericRepository<TEntity> repository, ILogger logger, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<object> GetEntitiesAsync(Type? destinationType = null)
        {
            var result = await _repository.GetAll();

            if (destinationType != null)
                return _mapper.Map(result, destinationType);

            return result;
        }

        public async Task<object> GetEntityAsync(
            Expression<Func<TEntity, bool>> filter, 
            Expression<Func<TEntity, object>>[]? includes = null, 
            Type? destinationType = null
            )
        {
            _logger.LogInformation($"{nameof(TEntity)} entity was requested");
            var result = await _repository.GetByQuery(filter, includes);

            if (result == null)
            {
                _logger.LogError($"{nameof(TEntity)} was not found");
                throw new NotFoundException();
            }

            if (destinationType != null)
                return _mapper.Map(result, destinationType);

            return result;
        }

        public async Task DeleteEntityAsync(Expression<Func<TEntity, bool>> filter)
        {
            _logger.LogInformation($"{nameof(TEntity)} entity was requested");
            var result = await _repository.GetByQuery(filter);

            if (result is null)
            {
                _logger.LogError($"{nameof(TEntity)} was not found");
                throw new NotFoundException();
            }

            await _repository.Delete(result);
        }

        public async Task<object> AddEntityAsync(TEntity entity, Type? destinationType = null)
        {
            var result = await _repository.Add(entity);

            _logger.LogInformation($"Product with the properties: {result} was added");

            if (destinationType != null)
                return _mapper.Map(result,destinationType);

            return result;
        }

        public async Task<object> UpdateEntityAsync(TEntity entity, Type? destinationType = null)
        {
            var result = await _repository.Update(entity);

            _logger.LogInformation($"Product with these properties: {result} has been updated");

            if (destinationType != null)
                return _mapper.Map(result, destinationType);

            return result;
        }

    }
}
