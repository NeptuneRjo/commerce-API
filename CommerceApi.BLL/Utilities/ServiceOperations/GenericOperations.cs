using CommerceApi.DAL.Repositories;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace CommerceApi.BLL.Utilities
{
    public class GenericOperations<TEntity> : IGenericOperations<TEntity>
    {
        private IGenericRepository<TEntity> _repository;
        private ILogger _logger;

        public GenericOperations(IGenericRepository<TEntity> repository, ILogger logger)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task DeleteEntityOperation(Expression<Func<TEntity, bool>> filter)
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

        public async Task<TEntity> AddEntityOperation(TEntity entity)
        {
            var result = await _repository.Add(entity);

            _logger.LogInformation($"Product with the properties: {result} was added");

            return result;
        }

        public async Task<TEntity> UpdateEntityOperation(TEntity entity)
        {
            var result = await _repository.Update(entity);

            _logger.LogInformation($"Product with these properties: {result} has been updated");

            return result;
        }

        public async Task<TEntity> RetrieveEntityOperation(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, object>>[]? includes = null
            )
        {
            _logger.LogInformation($"{nameof(TEntity)} entity was requested");
            var result = await _repository.GetByQuery(filter, includes);

            if (result == null)
            {
                _logger.LogError($"{nameof(TEntity)} was not found");
                throw new NotFoundException();
            }

            return result;
        }

        public async Task<ICollection<TEntity>> RetrieveEntitiesOperation()
        {
            var result = await _repository.GetAll();

            return result;
        }
    }
}
