using AutoMapper;
using CommerceApi.BLL.Services;
using CommerceApi.BLL.Utilities;
using CommerceApi.BLL.Utilities.CustomExceptions;
using CommerceApi.DAL.Entities;
using CommerceApi.DAL.Repositories;
using CommerceApi.DTO.DTOS;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Engine.ClientProtocol;
using NSubstitute;
using System.Linq.Expressions;
using Xunit.Abstractions;

namespace CommerceApi.Test.Services
{
    public class ProductServiceTests
    {
        private readonly ITestOutputHelper _output;

        private readonly IProductService _service;
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;

        private const string ProductId = "PROD_12345";
        private readonly Product _productEntity;
        private readonly ProductToAddDto _productToAddDto;
        private readonly ProductToUpdateDto _productToUpdateDto;
        private readonly ProductDto _productDto;

        public ProductServiceTests(ITestOutputHelper output)
        {
            _repository = Substitute.For<IProductRepository>();
            _logger = Substitute.For<ILogger<ProductService>>();

            var profile = new AutoMapperProfiles();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));

            _mapper = new Mapper(configuration);

            _service = new ProductService(_repository, _mapper, _logger);

            _productEntity = new Product()
            {
                ProductId = ProductId,
                Name = "ProductEntityName",
                Description = "ProductEntityDesc",
                Price = 0,
                StockQuantity = 0,
                Brand = "ProductEntityBrand",
                ProductReviews = new List<ProductReview>(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            _productDto = new ProductDto()
            {
                ProductId = ProductId,
                Name = "ProductEntityName",
                Description = "ProductEntityDesc",
                Price = 0,
                StockQuantity = 0,
                Brand = "ProductEntityBrand",
                ProductReviews = new List<ProductReviewDto>(),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            _productToAddDto = new ProductToAddDto()
            {
                Name = "ProductToAddName",
                Description = "ProductToAddDesc",
                Price = 0,
                Category = "ProductToAddCat",
                Brand = "ProductToAddBrand",
                StockQuantity = 0,
                InStock = false,
                Currency = "ProductToAddCur"
            };

            _productToUpdateDto = new ProductToUpdateDto()
            {
                ProductId = ProductId,
                Name = "ProductToUpdateName",
                Description = "ProductToUpdateDesc",
                Price = 0,
                Category = "ProductToUpdateCat",
                Brand = "ProductToUpdateBrand",
                StockQuantity = 0,
                InStock = false,
                Currency = "ProductToUpdateCur"
            };

            _output = output;
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
