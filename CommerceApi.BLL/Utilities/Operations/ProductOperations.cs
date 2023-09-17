using CommerceApi.DAL.Entities;
using CommerceApi.DAL.Repositories;
using CommerceApi.DTO.DTOS;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceApi.BLL.Utilities
{
    public class ProductOperations : GenericOperations<Product>, IProductOperations
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductOperations> _logger;

        public ProductOperations(IProductRepository repository, ILogger<ProductOperations> logger) : base(repository, logger) 
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Product> AddProductOperation(Product product)
        {
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;

            product.ProductReviews = new List<ProductReview>();
            product.CartProducts = new List<CartProduct>();
            product.WishListProducts = new List<WishListProduct>();

            Product addedProduct = await _repository.Add(product);

            _logger.LogInformation($"Product with the properties: {addedProduct} was added");

            return addedProduct;
        }
    }
}
