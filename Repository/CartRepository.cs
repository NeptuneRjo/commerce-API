using CommerceClone.Interfaces;
using CommerceClone.Models;

namespace CommerceClone.Repository
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {

        private readonly IDataContext _context;

        public CartRepository(IDataContext context) : base(context)
        {
            _context = context;
        }

        public Cart AddItem(Cart cart, int itemId)
        {
            var item = _context.Items.Find(itemId);

            cart.Items.Add(item);

            return cart;
        }

        public void AddToStore(int storeId, Cart cart)
        {
            var store = _context.Stores.FirstOrDefault(e => e.Id == storeId);

            _context.Carts.Add(cart);
            store.Carts.Add(cart);

            _context.SaveChanges();
        }

        public Cart GetByUid(string uid)
        {
            return _context.Carts.FirstOrDefault(e => e.Uid == uid);
        }

        public Cart RemoveItemFromCart(Cart cart, int itemId)
        {
            var item = _context.Items.Find(itemId);

            cart.Items.Remove(item);

            return cart;
        }
    }
}
