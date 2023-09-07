using CommerceApi.DTO.DTOS;

namespace CommerceApi.BLL.Services
{
    public interface IProductService
    {
        Task<ICollection<ProductDto>> GetProductsAsync();

        Task<ProductDto> GetProductAsync(string id);

        Task<ProductDto> AddProductAsync(ProductToAddDto productToAddDto);

        Task<ProductDto> UpdateProductAsync(ProductToUpdateDto productToUpdateDto);

        Task DeleteProductAsync(string id);
    }
}
