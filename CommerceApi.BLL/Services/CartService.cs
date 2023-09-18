using CommerceApi.DAL.Entities;
using AutoMapper;
using CommerceApi.BLL.Utilities.Operations;
using CommerceApi.DTO.DTOS;

namespace CommerceApi.BLL.Services
{
    public class CartService : GenericService<Cart>, ICartService
    {
        private readonly IMapper _mapper;
        private readonly ICartOperations _ops;

        public CartService(IMapper mapper, ICartOperations ops) : base(mapper, ops)
        {
            _mapper = mapper;
            _ops = ops;
        }

        public async Task<CartDto> UpdateCartAsync(string id, CartDto update) =>
            _mapper.Map<CartDto>(
                _ops.UpdateEntityOperation(e => e.UID == id, 
                    // Map the dto to the entity
                    _mapper.Map(update, 
                        await _ops.RetrieveEntityOperation(e => e.UID == id))));
    }
}
