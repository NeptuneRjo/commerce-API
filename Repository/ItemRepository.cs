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

        public Item AddByEmail(string email, int storeId, Item item)
        {
            Store store = _context.Stores.Find(storeId);

            item.Store = store;

            _context.Items.Add(item);

            store.Items.Add(item);

            _context.SaveChanges();

            return item;
        }

        public Item AddByKey(string sk, int storeId, Item item)
        {
            Store store = _context.Stores.Find(storeId);

            item.Store = store;

            _context.Items.Add(item);

            store.Items.Add(item);

            _context.SaveChanges();

            return item;
        }
    }
}
