using AutoMapper;
using CommerceApi.DAL.Entities;
using CommerceApi.DAL.Repositories;
using CommerceApi.DTO.DTOS;
using CommerceApi.BLL.Utilities;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace CommerceApi.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository repository, IMapper mapper, ILogger<ProductService> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
        }

        public async Task<ICollection<ProductDto>> GetProductsAsync()
        {
            ICollection<Product> products = await _repository.GetAll();

            _logger.LogInformation($"List of {products.Count} products has been returned");

            return _mapper.Map<ICollection<ProductDto>>(products); ;
        }

        public async Task<ProductDto> GetProductAsync(string id)
        {
            _logger.LogInformation($"Product with the ProductId = {id} was requested");

            Expression<Func<Product, bool>> query = e => e.ProductId == id;
            Expression<Func<Product, object>>[] includes = { e => e.ProductReviews };

            Product product = await _repository.GetProductAsync(id);

            if (product is null)
            {
                _logger.LogError($"Product with the ProductId = {id} was not found");
                throw new NotFoundException();
            }

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> AddProductAsync(ProductToAddDto productToAddDto)
        {
            Product product = _mapper.Map<Product>(productToAddDto);

            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;

            product.ProductReviews = new List<ProductReview>();
            product.CartProducts = new List<CartProduct>();
            product.WishListProducts = new List<WishListProduct>();

            Product addedProduct = await _repository.Add(product);

            _logger.LogInformation($"Product with the properties: {addedProduct} was added");

            return _mapper.Map<ProductDto>(addedProduct);
        }

        public async Task<ProductDto> UpdateProductAsync(ProductToUpdateDto productToUpdateDto)
        {
            Product product = await _repository.GetProductAsync(productToUpdateDto.ProductId);

            if (product is null)
            {
                _logger.LogError($"Product with the ProductId = {productToUpdateDto.ProductId} was not found");
                throw new NotFoundException();
            }

            Product productToUpdate = _mapper.Map<Product>(productToUpdateDto);

            _logger.LogInformation($"Product with these properties: {productToUpdateDto} has been updated");

            Product updatedProduct = await _repository.Update(productToUpdate);

            return _mapper.Map<ProductDto>(updatedProduct);
        }

        public async Task DeleteProductAsync(string id)
        {
            Product productToDelete = await _repository.GetProductAsync(id);

            if (productToDelete is null) 
            {
                _logger.LogError($"Product with the ProductId = {id} was not found");
                throw new NotFoundException();
            }

            await _repository.Delete(productToDelete);
        }
    }
}
