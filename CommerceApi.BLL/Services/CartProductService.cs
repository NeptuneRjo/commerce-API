using AutoMapper;
using CommerceApi.BLL.Utilities.Operations;
using CommerceApi.DAL.Entities;
using CommerceApi.DTO.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceApi.BLL.Services
{
    internal class CartProductService : GenericService<CartProduct>, ICartProductService
    {
        private readonly IMapper _mapper;
        private readonly ICartProductOperations _ops;

        public CartProductService(IMapper mapper, ICartProductOperations ops) : base(mapper, ops)
        {
            _mapper = mapper;
            _ops = ops;
        }

        public async Task AddCartProductAsync(string id, CartProductToAddDto productToAdd) =>
            await _ops.AddEntityOperation(_mapper.Map<CartProduct>(productToAdd));
    }
}
