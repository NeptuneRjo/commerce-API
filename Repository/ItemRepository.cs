using AutoMapper;
using CommerceClone.Interfaces;
using CommerceClone.Models;

namespace CommerceClone.Repository
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public ItemRepository(IDataContext context, IMapper mapper) : base(context, mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        
    }
}
