using AutoMapper;
using CommerceClone.Interfaces;
using CommerceClone.Models;

namespace CommerceClone.Repository
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {

        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public CartRepository(IDataContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Cart AddItem(Cart cart, int itemId, int quantity)
        {
            var item = _context.Items.Find(itemId);
            var cartItem = new CartItem()
            {
                Item = item,
                Quantity = quantity,
                CartId = cart.Id
            };

            cart.CartItems.Add(cartItem);
            _context.SaveChanges();

            return cart;
        }

        public Cart ClearItems(Cart cart)
        {
            cart.CartItems = new List<CartItem>();

            _context.Carts.Update(cart);
            _context.SaveChanges();

            return cart;
        }

        public ICollection<CartItem> RemoveItem(ICollection<CartItem> items, int itemId, int quantity)
        {
            var item = items.First(e => e.Item.Id == itemId);

            item.Quantity = item.Quantity - quantity;

            if (item.Quantity < 1)
                _context.CartItems.Remove(item);

            _context.CartItems.Update(item);
            _context.SaveChanges();

            return items;
        }
    }
}
