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

        public void AddToStore(string sk, int storeId, Item item)
        {
            var store = _context.Stores
                .Where(e => e.Admin.SecretKey == sk)
                .FirstOrDefault(e => e.Id == storeId);
                

            _context.Items.Add(item);
            store.Items.Add(item);

            _context.SaveChanges();
        }

        public ICollection<Item> GetByStore(int storeId, string pk)
        {
            return _context.Items
                .Where(e => e.Store.Admin.PublicKey == pk)
                .Where(e => e.StoreId == storeId).ToList();
        }

        public Item GetItemByName(string name)
        {
            return _context.Items.Find(name);
        }

        public Item GetById(string pk, int storeId, int itemId)
        {
            return _context.Items
                .Where(e => e.Store.Admin.PublicKey == pk)
                .Where(e => e.StoreId == storeId)
                .FirstOrDefault(e => e.Id == itemId);
        }
    }
}
