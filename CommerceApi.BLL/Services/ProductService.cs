using AutoMapper;
using CommerceApi.DAL.Entities;
using CommerceApi.DTO.DTOS;
using CommerceApi.BLL.Utilities;

namespace CommerceApi.BLL.Services
{
    public class ProductService : GenericService<Product>, IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductOperations _ops;

        public ProductService(IMapper mapper, IProductOperations ops) : base(mapper, ops)
        {
            _mapper = mapper;
            _ops = ops;
        }

        public async Task<ProductDto> AddProductAsync(ProductToAddDto productToAddDto) =>
            _mapper.Map<ProductDto>(await _ops.AddProductOperation(_mapper.Map<Product>(productToAddDto)));

        public async Task<ProductDto> UpdateProductAsync(string id, ProductToUpdateDto productToUpdate) =>
            _mapper.Map<ProductDto>(await _ops.UpdateEntityOperation(e => e.UID == id, _mapper.Map<Product>(productToUpdate)));
    }
}
