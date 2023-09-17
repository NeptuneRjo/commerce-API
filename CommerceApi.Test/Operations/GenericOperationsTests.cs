using CommerceApi.BLL.Services;
using CommerceApi.BLL.Utilities;
using CommerceApi.DAL.Entities;
using CommerceApi.DAL.Repositories;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Linq.Expressions;
using Xunit.Abstractions;

namespace CommerceApi.Test.Operations
{
    public class GenericOperationsTests : TestUtilities
    {

        private readonly ITestOutputHelper _output;

        private readonly IGenericOperations<MockEntity> _ops;
        private readonly IGenericRepository<MockEntity> _repository;
        private readonly ILogger<GenericOperations<MockEntity>> _logger;

        private readonly Expression<Func<MockEntity, bool>> _queryById;
        private readonly Expression<Func<MockEntity, bool>> _queryByName;

        private readonly MockEntity _entity;

        public GenericOperationsTests(ITestOutputHelper output)
        {
            _output = output;
            _repository = Substitute.For<IGenericRepository<MockEntity>>();
            _logger = Substitute.For<ILogger<GenericOperations<MockEntity>>>();

            _ops = new GenericOperations<MockEntity>(_repository, _logger);

            _queryById = e => e.Id == 1;
            _queryByName = e => e.Name == "MockEntity";
            _entity = new MockEntity()
            {
                Id = 1,
                Name = "MockEntity"
            };
        }

        [Fact]
        public async Task RetrieveEntityOperation_WhenSuccess_ReturnsEntity()
        {
            // `Includes` MUST be null or the method wont match the one in GetEntityAsync()
            _repository.GetByQuery(_queryByName, null).Returns(Task.FromResult(_entity));

            var result = await _ops.RetrieveEntityOperation(_queryByName);

            Assert.Equivalent(_entity, result, strict: true);
        }

        [Fact]
        public async Task RetrieveEntityOperation_WhenEntityDoesNotExist_ThrowsNotFoundException()
        {
            _repository.GetByQuery(_queryByName, null).Returns(Task.FromResult((MockEntity)null!));

            await Assert.ThrowsAsync<NotFoundException>(() => _ops.RetrieveEntityOperation(_queryByName));
        }

        [Fact]
        public async Task RetrieveEntitiesOperation_WhenSuccess_ReturnsEntities()
        {
            ICollection<MockEntity> entities = new List<MockEntity>() { _entity, _entity };

            _repository.GetAll().Returns(Task.FromResult(entities));

            var result = await _ops.RetrieveEntitiesOperation();

            Assert.Equivalent(result, entities, strict: true);
        }

        [Fact]
        public async Task UpdateEntityOperation_WhenSuccess_ReturnsEntity()
        {
            _repository.GetByQuery(_queryByName).Returns(Task.FromResult(_entity));
            _repository.Update(_entity).Returns(Task.FromResult(_entity));

            var result = await _ops.UpdateEntityOperation(_queryByName, _entity);

            Assert.Equivalent(result, _entity, strict: true);
        }

        [Fact]
        public async Task UpdateEntityOperation_WhenEntityDoesNotExist_ThrowsNotFoundException()
        {
            _repository.GetByQuery(_queryByName).Returns(Task.FromResult((MockEntity)null!));
            _repository.Update(_entity).Returns(Task.FromResult(_entity));

            await Assert.ThrowsAsync<NotFoundException>(() => _ops.UpdateEntityOperation(_queryByName, _entity));
        }

        [Fact]
        public async Task DeleteEntityOperation_WhenSuccess_DeletesEntity()
        {
            _repository.GetByQuery(_queryByName).Returns(Task.FromResult(_entity));

            await _ops.DeleteEntityOperation(_queryByName);

            await _repository.Received().Delete(Arg.Any<MockEntity>());
        }

        [Fact]
        public async Task DeleteEntityOperation_WhenEntityNotFound_ThrowsNotFoundException()
        {
            _repository.GetByQuery(_queryByName).Returns(Task.FromResult((MockEntity)null!));

            await Assert.ThrowsAsync<NotFoundException>(() => _ops.DeleteEntityOperation(_queryByName));
        }

        [Fact]
        public async Task AddEntityOperation_WhenSuccess_ReturnsEntity()
        {
            _repository.Add(_entity).Returns(Task.FromResult(_entity));

            var result = await _ops.AddEntityOperation(_entity);

            Assert.Equivalent(result, _entity, strict: true);
        }
    }
}
