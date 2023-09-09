using CommerceApi.BLL.Services;
using CommerceApi.BLL.Utilities;
using CommerceApi.Controllers;
using CommerceApi.DAL.Entities;
using CommerceApi.DTO.DTOS;
using CommerceApi.Test.Utilities;
using NSubstitute;
using Xunit.Abstractions;

namespace CommerceApi.Test.Controllers
{
    public class ProductControllerTests : TestUtilities
    {
        private readonly ITestOutputHelper _output;
        private readonly IProductService _service;
        private readonly ProductController _controller;

        private readonly ProductEntityUtilities _utils;

        public ProductControllerTests(ITestOutputHelper output)
        {
            _output = output;
            _service = Substitute.For<IProductService>();
            _controller = new ProductController(_service);

            _utils = new ProductEntityUtilities();
        }

        [Fact]
        public async Task GetProductsAsync_ReturnsSuccess()
        {
            ICollection<ProductDto> productDtos = new List<ProductDto>() { _utils.ProductDto, _utils.ProductDto };

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
            _service.GetProductAsync(_utils.ProductId).Returns(Task.FromResult(_utils.ProductDto));

            var result = await ReturnsOk(e => e.GetProductAsync(_utils.ProductId), _controller);

            // Test the content of the response
            var returnedDto = Assert.IsAssignableFrom<ProductDto>(result.Value);

            Assert.NotNull(returnedDto);
            Assert.Equal(returnedDto, _utils.ProductDto);
        }

        [Fact]
        public async Task GetProductAsync_ReturnsNotFound()
        {
            var exception = Task.FromException<ProductDto>(new NotFoundException());
            _service.GetProductAsync(_utils.ProductId).Returns(exception);

            var result = await ReturnsNotFound(e => e.GetProductAsync(_utils.ProductId), _controller);

            Assert.NotEqual(result.Value, string.Empty);
        }

        [Fact]
        public async Task GetProductAsync_ReturnsBadRequest()
        {
            var exception = Task.FromException<ProductDto>(new Exception());
            _service.GetProductAsync(_utils.ProductId).Returns(exception);

            var result = await ReturnsBadRequest(e => e.GetProductAsync(_utils.ProductId), _controller);

            Assert.NotEqual(result.Value, string.Empty);
        }

        [Fact]
        public async Task UpdateProductAsync_ReturnsSuccess()
        {
            _service.UpdateProductAsync(_utils.ProductToUpdateDto).Returns(Task.FromResult(_utils.ProductDto));

            var result = await ReturnsOk(e => e.UpdateProductAsync(_utils.ProductId, _utils.ProductToUpdateDto), _controller);

            // Test the content of the response
            var returnedDto = Assert.IsAssignableFrom<ProductDto>(result.Value);

            Assert.NotNull(returnedDto);
            Assert.Equal(returnedDto, _utils.ProductDto);
        }

        [Fact]
        public async Task UpdateProductAsync_ReturnsNotFound()
        {
            var exception = Task.FromException<ProductDto>(new NotFoundException());
            _service.UpdateProductAsync(_utils.ProductToUpdateDto).Returns(exception);

            var result = await ReturnsNotFound(e => e.UpdateProductAsync(_utils.ProductId, _utils.ProductToUpdateDto), _controller);

            Assert.NotEqual(result.Value, string.Empty);
        }

        [Fact]
        public async Task UpdateProductAsync_ReturnsBadRequest()
        {
            var exception = Task.FromException<ProductDto>(new Exception());
            _service.UpdateProductAsync(_utils.ProductToUpdateDto).Returns(exception);

            var result = await ReturnsBadRequest(e => e.UpdateProductAsync(_utils.ProductId, _utils.ProductToUpdateDto), _controller);

            Assert.NotEqual(result.Value, string.Empty);
        }

        [Fact]
        public async Task UpdateProductAsync_WhenModelStateIsInvalid_ReturnsBadRequest()
        {
            ProductToUpdateDto toAddDto = new ProductToUpdateDto();

            var result = await ReturnsBadRequest(e => e.UpdateProductAsync(_utils.ProductId, toAddDto), _controller);

            Assert.NotEqual(result.Value, string.Empty);
        }

        [Fact]
        public async Task UpdateProductAsync_WhenIdMistmatch_ReturnsBadRequest()
        {
            var result = await ReturnsBadRequest(e => e.UpdateProductAsync("BAD_ID", _utils.ProductToUpdateDto), _controller);

            Assert.NotEqual(result.Value, string.Empty);
        }

        [Fact]
        public async Task DeleteProductAsync_ReturnsSuccess()
        {
            _service.DeleteProductAsync(_utils.ProductId).Returns(Task.CompletedTask);

            var result = await ReturnsOk(e => e.DeleteProductAsync(_utils.ProductId), _controller);

            Assert.NotEqual(result.Value, string.Empty);
        }

        [Fact]
        public async Task DeleteProductAsync_ReturnsNotFound()
        {
            var exception = Task.FromException<Task>(new NotFoundException());
            _service.DeleteProductAsync(_utils.ProductId).Returns(exception);

            var result = await ReturnsNotFound(e => e.DeleteProductAsync(_utils.ProductId), _controller);

            Assert.NotEqual(result.Value, string.Empty);
        }

        [Fact]
        public async Task DeleteProductAsync_ReturnsBadRequest()
        {
            var exception = Task.FromException<Task>(new Exception());
            _service.DeleteProductAsync(_utils.ProductId).Returns(exception);

            var result = await ReturnsBadRequest(e => e.DeleteProductAsync(_utils.ProductId), _controller);

            Assert.NotEqual(result.Value, string.Empty);
        }

        [Fact]
        public async Task AddProductAsync_ReturnsSuccess()
        {
            _service.AddProductAsync(_utils.ProductToAddDto).Returns(Task.FromResult(_utils.ProductDto));

            var result = await ReturnsOk(e => e.AddProductAsync(_utils.ProductToAddDto), _controller);

            // Test the content of the response
            var returnedDto = Assert.IsAssignableFrom<ProductDto>(result.Value);

            Assert.NotNull(returnedDto);
            Assert.Equal(returnedDto, _utils.ProductDto);
        }

        [Fact]
        public async Task AddProductAsync_ReturnsBadRequest()
        {
            var exception = Task.FromException<ProductDto>(new Exception());
            _service.AddProductAsync(_utils.ProductToAddDto).Returns(exception);

            var result = await ReturnsBadRequest(e => e.AddProductAsync(_utils.ProductToAddDto), _controller);

            Assert.NotEqual(result.Value, string.Empty);
        }
    }
}
