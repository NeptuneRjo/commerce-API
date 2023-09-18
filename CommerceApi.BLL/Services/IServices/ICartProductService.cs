using CommerceApi.DAL.Entities;
using CommerceApi.DTO.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceApi.BLL.Services
{
    public interface ICartProductService : IGenericService<CartProduct>
    {
        Task AddCartProductAsync(string cartId, CartProductToAddDto productToAdd);

    }
}
