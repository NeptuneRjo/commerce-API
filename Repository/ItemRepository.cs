using CommerceClone.Interfaces;
using CommerceClone.Models;

namespace CommerceClone.Repository
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        private readonly IDataContext _context;

        public ItemRepository(IDataContext context) : base(context) 
        {
            _context = context;
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
