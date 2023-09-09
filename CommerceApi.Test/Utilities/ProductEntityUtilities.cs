using CommerceApi.DAL.Entities;
using CommerceApi.DTO.DTOS;

namespace CommerceApi.Test.Utilities
{
    public class ProductEntityUtilities
    {
        public string ProductId = "PROD_12345";

        public readonly Product ProductEntity;
        public readonly ProductDto ProductDto;
        public readonly ProductToAddDto ProductToAddDto;
        public readonly ProductToUpdateDto ProductToUpdateDto;

        public ProductEntityUtilities()
        {
            ProductEntity = new()
            {
                ProductId = ProductId,
                Name = "ProductEntityName",
                Description = "ProductEntityDesc",
                Price = 0,
                StockQuantity = 0,
                Brand = "ProductEntityBrand",
                ProductReviews = new List<ProductReview>(),
                CreatedAt = new DateTime(),
                UpdatedAt = new DateTime(),
            };

            ProductDto = new()
            {
                ProductId = ProductId,
                Name = "ProductEntityName",
                Description = "ProductEntityDesc",
                Price = 0,
                StockQuantity = 0,
                Brand = "ProductEntityBrand",
                ProductReviews = new List<ProductReviewDto>(),
                CreatedAt = new DateTime(),
                UpdatedAt = new DateTime(),
            };

            ProductToAddDto = new()
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

            ProductToUpdateDto = new()
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
        }
    }
}
