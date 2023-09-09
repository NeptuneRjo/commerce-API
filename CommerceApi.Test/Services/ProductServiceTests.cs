using AutoMapper;
using CommerceApi.BLL.Services;
using CommerceApi.BLL.Utilities;
using CommerceApi.DAL.Entities;
using CommerceApi.DAL.Repositories;
using CommerceApi.DTO.DTOS;
using CommerceApi.Test.Utilities;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit.Abstractions;

namespace CommerceApi.Test.Services
{
    public class ProductServiceTests : TestUtilities
    {
        private readonly ITestOutputHelper _output;

        private readonly IProductService _service;
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductService> _logger;

        private readonly ProductEntityUtilities _utils;

        public ProductServiceTests(ITestOutputHelper output)
        {
            _output = output;
            _repository = Substitute.For<IProductRepository>();
            _logger = Substitute.For<ILogger<ProductService>>();

            _service = new ProductService(_repository, _mapper, _logger);

            _utils = new ProductEntityUtilities();
        }

        [Fact]
        public async Task GetProductsAsync_WhenSuccess_ReturnsProductDtoList()
        {
            ICollection<Product> products = new List<Product>() { _utils.ProductEntity, _utils.ProductEntity };
            ICollection<ProductDto> productsDto = new List<ProductDto>() { _utils.ProductDto, _utils.ProductDto };

            _repository.GetAll().Returns(Task.FromResult(products));

            var result = await _service.GetProductsAsync();

            Assert.Equal(2, result.Count);
            Assert.Equivalent(productsDto, result, strict: true);
        }

        [Fact]
        public async Task GetProductAsync_WhenSuccess_ReturnsProductDto()
        {
            _repository.GetProductAsync(_utils.ProductId).Returns(Task.FromResult(_utils.ProductEntity));

            var result = await _service.GetProductAsync(_utils.ProductId);

            Assert.NotNull(result);
            Assert.Equivalent(_utils.ProductDto, result, strict: true);
        }

        [Fact]
        public async Task GetProductAsync_WhenProductDoesNotExist_ThrowsNotFoundException()
        {
            _repository.GetProductAsync(_utils.ProductId).Returns(Task.FromResult((Product)null!));

            await Assert.ThrowsAsync<NotFoundException>(() => _service.GetProductAsync(_utils.ProductId));
        }

        [Fact]
        public async Task AddProductAsync_WhenSuccess_AddsThenReturnsProductDto()
        {
            _repository.Add(Arg.Any<Product>()).Returns(Task.FromResult(_utils.ProductEntity));

            var result = await _service.AddProductAsync(_utils.ProductToAddDto);

            Assert.IsType<ProductDto>(result);
            Assert.Equal(_utils.ProductEntity.ProductId, result.ProductId);
        }

        [Fact]
        public async Task UpdateProductAsync_WhenSuccess_UpdatesThenReturnsProductDto()
        {
            _repository.GetProductAsync(_utils.ProductId).Returns(Task.FromResult(_utils.ProductEntity));
            _repository.Update(Arg.Any<Product>()).Returns(Task.FromResult(_utils.ProductEntity));

            var result = await _service.UpdateProductAsync(_utils.ProductToUpdateDto);

            Assert.IsType<ProductDto>(result);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateProductAsync_WhenProductDoesNotExist_ThrowsNotFoundException()
        {
            _repository.GetProductAsync(_utils.ProductId).Returns(Task.FromResult((Product)null!));

            await Assert.ThrowsAsync<NotFoundException>(() => _service.UpdateProductAsync(_utils.ProductToUpdateDto));
        }

        [Fact]
        public async Task DeleteProductAsync_WhenSuccess_CallsDelete()
        {
            _repository.GetProductAsync(_utils.ProductId).Returns(Task.FromResult(_utils.ProductEntity));

            await _service.DeleteProductAsync(_utils.ProductId);

            await _repository.Received().Delete(Arg.Any<Product>());
        }

        [Fact]
        public async Task DeleteProductAsync_WhenProductDoesNotExists_ThrowsNotFoundException()
        {
            _repository.GetProductAsync(_utils.ProductId).Returns(Task.FromResult((Product)null!));

            await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteProductAsync(_utils.ProductId));
        }
    }
}
