using CommerceApi.BLL.Services;
using CommerceApi.BLL.Utilities;
using CommerceApi.Controllers;
using CommerceApi.DAL.Entities;
using CommerceApi.DTO.DTOS;
using NSubstitute;
using Xunit.Abstractions;

namespace CommerceApi.Test.Controllers
{
    public class ProductControllerTests : TestUtilities
    {
        private readonly ITestOutputHelper _output;
        private readonly IProductService _service;
        private readonly ProductController _controller;

        public ProductControllerTests(ITestOutputHelper output)
        {
            _output = output;
            _service = Substitute.For<IProductService>();
            _controller = new ProductController(_service);
        }

        [Fact]
        public async Task GetProductsAsync_ReturnsSuccess()
        {
            ICollection<ProductDto> productDtos = new List<ProductDto>() { _productDto, _productDto };

            _service.GetProductsAsync().Returns(Task.FromResult(productDtos));

            var result = await ReturnsOk(e => e.GetProductsAsync(), _controller);

            // Test the content of the response
            var returnedDtos = Assert.IsAssignableFrom<ICollection<ProductDto>>(result.Value);

            Assert.NotEmpty(returnedDtos);
            Assert.All(productDtos, dto => Assert.Contains(dto, returnedDtos));
        }

        [Fact]
        public async Task GetProductsAsync_ReturnsBadRequest()
        {
            var exception = Task.FromException<ICollection<ProductDto>>(new Exception());
            _service.GetProductsAsync().Returns(exception);

            await ReturnsBadRequest(e => e.GetProductsAsync(), _controller);
        }

        [Fact]
        public async Task GetProductAsync_ReturnsSuccess()
        {
            _service.GetProductAsync(ProductId).Returns(Task.FromResult(_productDto));

            var result = await ReturnsOk(e => e.GetProductAsync(ProductId), _controller);

            // Test the content of the response
            var returnedDto = Assert.IsAssignableFrom<ProductDto>(result.Value);

            Assert.NotNull(returnedDto);
            Assert.Equal(returnedDto, _productDto);
        }

        [Fact]
        public async Task GetProductAsync_ReturnsNotFound()
        {
            var exception = Task.FromException<ProductDto>(new NotFoundException());
            _service.GetProductAsync(ProductId).Returns(exception);

            var result = await ReturnsNotFound(e => e.GetProductAsync(ProductId), _controller);

            Assert.NotEqual(result.Value, string.Empty);
        }

        [Fact]
        public async Task GetProductAsync_ReturnsBadRequest()
        {
            var exception = Task.FromException<ProductDto>(new Exception());
            _service.GetProductAsync(ProductId).Returns(exception);

            var result = await ReturnsBadRequest(e => e.GetProductAsync(ProductId), _controller);

            Assert.NotEqual(result.Value, string.Empty);
        }

        [Fact]
        public async Task UpdateProductAsync_ReturnsSuccess()
        {
            _service.UpdateProductAsync(_productToUpdateDto).Returns(Task.FromResult(_productDto));

            var result = await ReturnsOk(e => e.UpdateProductAsync(ProductId, _productToUpdateDto), _controller);

            // Test the content of the response
            var returnedDto = Assert.IsAssignableFrom<ProductDto>(result.Value);

            Assert.NotNull(returnedDto);
            Assert.Equal(returnedDto, _productDto);
        }

        [Fact]
        public async Task UpdateProductAsync_ReturnsNotFound()
        {
            var exception = Task.FromException<ProductDto>(new NotFoundException());
            _service.UpdateProductAsync(_productToUpdateDto).Returns(exception);

            var result = await ReturnsNotFound(e => e.UpdateProductAsync(ProductId, _productToUpdateDto), _controller);

            Assert.NotEqual(result.Value, string.Empty);
        }

        [Fact]
        public async Task UpdateProductAsync_ReturnsBadRequest()
        {
            var exception = Task.FromException<ProductDto>(new Exception());
            _service.UpdateProductAsync(_productToUpdateDto).Returns(exception);

            var result = await ReturnsBadRequest(e => e.UpdateProductAsync(ProductId, _productToUpdateDto), _controller);

            Assert.NotEqual(result.Value, string.Empty);
        }

        [Fact]
        public async Task UpdateProductAsync_WhenModelStateIsInvalid_ReturnsBadRequest()
        {
            ProductToUpdateDto toAddDto = new ProductToUpdateDto();

            var result = await ReturnsBadRequest(e => e.UpdateProductAsync(ProductId, toAddDto), _controller);

            Assert.NotEqual(result.Value, string.Empty);
        }

        [Fact]
        public async Task UpdateProductAsync_WhenIdMistmatch_ReturnsBadRequest()
        {
            var result = await ReturnsBadRequest(e => e.UpdateProductAsync("BAD_ID", _productToUpdateDto), _controller);

            Assert.NotEqual(result.Value, string.Empty);
        }

        [Fact]
        public async Task DeleteProductAsync_ReturnsSuccess()
        {
            _service.DeleteProductAsync(ProductId).Returns(Task.CompletedTask);

            var result = await ReturnsOk(e => e.DeleteProductAsync(ProductId), _controller);

            Assert.NotEqual(result.Value, string.Empty);
        }

        [Fact]
        public async Task DeleteProductAsync_ReturnsNotFound()
        {
            var exception = Task.FromException<Task>(new NotFoundException());
            _service.DeleteProductAsync(ProductId).Returns(exception);

            var result = await ReturnsNotFound(e => e.DeleteProductAsync(ProductId), _controller);

            Assert.NotEqual(result.Value, string.Empty);
        }

        [Fact]
        public async Task DeleteProductAsync_ReturnsBadRequest()
        {
            var exception = Task.FromException<Task>(new Exception());
            _service.DeleteProductAsync(ProductId).Returns(exception);

            var result = await ReturnsBadRequest(e => e.DeleteProductAsync(ProductId), _controller);

            Assert.NotEqual(result.Value, string.Empty);
        }

        [Fact]
        public async Task AddProductAsync_ReturnsSuccess()
        {
            _service.AddProductAsync(_productToAddDto).Returns(Task.FromResult(_productDto));

            var result = await ReturnsOk(e => e.AddProductAsync(_productToAddDto), _controller);

            // Test the content of the response
            var returnedDto = Assert.IsAssignableFrom<ProductDto>(result.Value);

            Assert.NotNull(returnedDto);
            Assert.Equal(returnedDto, _productDto);
        }

        [Fact]
        public async Task AddProductAsync_ReturnsBadRequest()
        {
            var exception = Task.FromException<ProductDto>(new Exception());
            _service.AddProductAsync(_productToAddDto).Returns(exception);

            var result = await ReturnsBadRequest(e => e.AddProductAsync(_productToAddDto), _controller);

            Assert.NotEqual(result.Value, string.Empty);
        }
    }
}
