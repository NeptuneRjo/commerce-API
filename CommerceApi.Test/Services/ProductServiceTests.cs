using AutoMapper;
using CommerceApi.BLL.Services;
using CommerceApi.BLL.Utilities;
using CommerceApi.DAL.Entities;
using CommerceApi.DAL.Repositories;
using CommerceApi.DTO.DTOS;
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
        private readonly IMapper _mapper;

        public ProductServiceTests(ITestOutputHelper output)
        {
            _output = output;

            _repository = Substitute.For<IProductRepository>();
            _logger = Substitute.For<ILogger<ProductService>>();

            var profile = new AutoMapperProfiles();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));

            _mapper = new Mapper(configuration);

            _service = new ProductService(_repository, _mapper, _logger);
        }

        [Fact]
        public async Task GetProductsAsync_WhenSuccess_ReturnsProductDtoList()
        {
            ICollection<Product> products = new List<Product>() { _productEntity, _productEntity };
            ICollection<ProductDto> productsDto = new List<ProductDto>() { _productDto, _productDto };

            _repository.GetAll().Returns(Task.FromResult(products));

            var result = await _service.GetProductsAsync();

            Assert.Equal(2, result.Count);
            Assert.Equivalent(productsDto, result, strict: true);
        }

        [Fact]
        public async Task GetProductAsync_WhenSuccess_ReturnsProductDto()
        {
            _repository.GetProductAsync(ProductId).Returns(Task.FromResult(_productEntity));

            var result = await _service.GetProductAsync(ProductId);

            Assert.NotNull(result);
            Assert.Equivalent(_productDto, result, strict: true);
        }

        [Fact]
        public async Task GetProductAsync_WhenProductDoesNotExist_ThrowsNotFoundException()
        {
            _repository.GetProductAsync(ProductId).Returns(Task.FromResult((Product)null!));

            await Assert.ThrowsAsync<NotFoundException>(() => _service.GetProductAsync(ProductId));
        }

        [Fact]
        public async Task AddProductAsync_WhenSuccess_AddsThenReturnsProductDto()
        {
            _repository.Add(Arg.Any<Product>()).Returns(Task.FromResult(_productEntity));

            var result = await _service.AddProductAsync(_productToAddDto);

            Assert.IsType<ProductDto>(result);
            Assert.Equal(_productEntity.ProductId, result.ProductId);
        }

        [Fact]
        public async Task UpdateProductAsync_WhenSuccess_UpdatesThenReturnsProductDto()
        {
            _repository.GetProductAsync(ProductId).Returns(Task.FromResult(_productEntity));
            _repository.Update(Arg.Any<Product>()).Returns(Task.FromResult(_productEntity));

            var result = await _service.UpdateProductAsync(_productToUpdateDto);

            Assert.IsType<ProductDto>(result);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task UpdateProductAsync_WhenProductDoesNotExist_ThrowsNotFoundException()
        {
            _repository.GetProductAsync(ProductId).Returns(Task.FromResult((Product)null!));

            await Assert.ThrowsAsync<NotFoundException>(() => _service.UpdateProductAsync(_productToUpdateDto));
        }

        [Fact]
        public async Task DeleteProductAsync_WhenSuccess_CallsDelete()
        {
            _repository.GetProductAsync(ProductId).Returns(Task.FromResult(_productEntity));

            await _service.DeleteProductAsync(ProductId);

            await _repository.Received().Delete(Arg.Any<Product>());
        }

        [Fact]
        public async Task DeleteProductAsync_WhenProductDoesNotExists_ThrowsNotFoundException()
        {
            _repository.GetProductAsync(ProductId).Returns(Task.FromResult((Product)null!));

            await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteProductAsync(ProductId));
        }
    }
}
