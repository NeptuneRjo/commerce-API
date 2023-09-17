using CommerceApi.BLL.Utilities;
using CommerceApi.DAL.Repositories;
using CommerceApi.Test.Utilities;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit.Abstractions;

namespace CommerceApi.Test.Operations
{
    public class ProductOperationTests : TestUtilities
    {
        private readonly ITestOutputHelper _output;

        private readonly IProductRepository _repository;
        private readonly ILogger<ProductOperations> _logger;

        private readonly ProductEntityUtilities _utils;

        private readonly IProductOperations _ops;

        public ProductOperationTests(ITestOutputHelper output)
        {
            _output = output;
            _repository = Substitute.For<IProductRepository>();
            _logger = Substitute.For<ILogger<ProductOperations>>();
            _utils = new ProductEntityUtilities();
            _ops = new ProductOperations(_repository, _logger);
        }

        [Fact]
        public async Task AddProductOperation_When_Success_ReturnsProduct()
        {
            _repository.Add(_utils.ProductEntity).Returns(Task.FromResult(_utils.ProductEntity));

            var result = await _ops.AddProductOperation(_utils.ProductEntity);

            Assert.Equivalent(result, _utils.ProductEntity, strict: true);
        }
    }
}
