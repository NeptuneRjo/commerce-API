using CommerceClone.Interfaces;
using CommerceClone.Models;

namespace CommerceClone.Repository
{
    public class StoreRepository : Repository<Store>, IStoreRepository
    {
        private readonly IDataContext _context;

        public StoreRepository(IDataContext context) : base(context)
        {
            _context = context;
        }

        public Store AddByKey(string sk, Store store)
        {
            Admin admin = _context.Admins.FirstOrDefault(e => e.SecretKey == sk);

            store.Admin = admin;

            _context.Stores.Add(store);

            admin.Stores.Add(store);

            _context.SaveChanges();

            return store;
        }

        public Store AddItem(Item item, int storeId)
        {
            Store store = _context.Stores.FirstOrDefault(e => e.Id == storeId);

            if (item.StoreId == null)
                item.StoreId = store.Id;

            item.Store = store;

            _context.Items.Add(item);

            store.Items.Add(item);

            _context.SaveChanges();

            return store;
        }

        public Cart AddCart(int id)
        {
            Store store = _context.Stores.First(e => e.Id == id);

            Cart cart = new Cart();
            cart.Store = store;

            _context.Carts.Add(cart);

            store.Carts.Add(cart);

            _context.SaveChanges();

            return cart;
        }
    }
}
