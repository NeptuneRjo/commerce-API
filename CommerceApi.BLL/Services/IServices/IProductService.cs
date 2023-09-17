using CommerceApi.DAL.Entities;
using CommerceApi.DTO.DTOS;

namespace CommerceApi.BLL.Services
{
    public interface IProductService : IGenericService<Product>
    {
        /// <summary>
        /// Add the product
        /// </summary>
        /// <param name="productToAdd"></param>
        /// <returns>The added <see cref="ProductDto"/></returns>
        Task<ProductDto> AddProductAsync(ProductToAddDto productToAdd);
        
        /// <summary>
        /// Update the product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productToUpdate"></param>
        /// <returns>The updated <see cref="ProductDto"/></returns>
        Task<ProductDto> UpdateProductAsync(string id, ProductToUpdateDto productToUpdate);
    }
}
