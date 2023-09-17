using CommerceApi.DAL.Entities;
using CommerceApi.DTO.DTOS;

namespace CommerceApi.BLL.Utilities
{
    public interface IProductOperations : IGenericOperations<Product>
    {
        Task<Product> AddProductOperation(Product productToAdd);

    }
}
