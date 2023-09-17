using CommerceApi.DAL.Entities;
using CommerceApi.DTO.DTOS;

namespace CommerceApi.BLL.Services
{
    public interface ICartService : IGenericService<Cart>
    {
        Task<CartDto> UpdateCartAsync(string id, CartDto update);
    }
}
