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

        public async Task<ICollection<TEntity>> GetEntitiesAsync() =>
            await _repository.GetAll();

        public async Task<ICollection<TDestination>> GetEntitiesAsync<TDestination>() =>
            _mapper.Map<ICollection<TDestination>>(await _repository.GetAll());

        public async Task<TEntity> GetEntityAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>>[]? includes = null)
        {
            _logger.LogInformation($"{nameof(TEntity)} entity was requested");
            var result = await _repository.GetByQuery(filter, includes);

            if (result is null)
            {
                _logger.LogError($"{nameof(TEntity)} was not found");
                throw new NotFoundException();
            }

            return result;
        }

        public async Task<TDestination> GetEntityAsync<TDestination>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>>[]? includes = null)
        {
            _logger.LogInformation($"{nameof(TEntity)} entity was requested");
            var result = await _repository.GetByQuery(filter, includes);

            if (result is null)
            {
                _logger.LogError($"{nameof(TEntity)} was not found");
                throw new NotFoundException();
            }

            return _mapper.Map<TDestination>(result);
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


    }
}
