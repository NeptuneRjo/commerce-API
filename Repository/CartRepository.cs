using CommerceClone.CustomExceptions;
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

        public Cart AddItem(Cart cart, CartItem cartItem)
        {
            Item item = _context.Items.Find(cartItem.ItemId);

            if (item == null)
                throw new ObjectNotFoundException($"No item with the id: {cartItem.ItemId} found");

            cartItem.CartId = cart.Id;
            cartItem.Price = item.Price;
            cartItem.Total = cartItem.Price * cartItem.Quantity;

            _context.CartItems.Add(cartItem);

            cart.CartItems.Add(cartItem);
            
            _context.SaveChanges();

            return cart;
        }

        public Cart ClearItems(Cart cart)
        {
            foreach (var item in cart.CartItems)
            {
                _context.CartItems.Remove(item);
            }

            _context.SaveChanges();

            return cart;
        }

        public Cart RemoveItem(int cartId, int itemId)
        {
            Cart cart = _context.Carts.Find(cartId);

            if (cart == null)
                throw new ObjectNotFoundException($"No cart with the id {cartId} found");

            CartItem cartItem = _context.CartItems.First(e => e.ItemId == itemId);
            
            if (cartItem == null)
                throw new ObjectNotFoundException($"No cart item with the item id {itemId} found");

            _context.CartItems.Remove(cartItem);
            _context.SaveChanges();

            return cart;
        }
       
        public Cart UpdateInfo(Cart cart)
        {

            cart.TotalItems = cart.CartItems.Sum(e => e.Quantity);
            cart.Subtotal = cart.CartItems.Sum(e => e.Total);

            cart.TotalUniqueItems = cart.CartItems.Count();

            _context.Carts.Update(cart);
            _context.SaveChanges();

            return cart;
        }

        public Cart UpdateItem(int cartId, int itemId, int quantity)
        {
            Cart cart = _context.Carts.Find(cartId);

            if (cart == null)
                throw new ObjectNotFoundException($"No cart with the id {cartId} found");

            CartItem cartItem = _context.CartItems.First(e => e.ItemId == itemId);
            Item item = _context.Items.First(e => e.Id == itemId);

            if (cartItem == null)
                throw new ObjectNotFoundException($"No item with the id {itemId} found in the cart");

            if (quantity == 0)
                _context.CartItems.Remove(cartItem);

            cartItem.Quantity = quantity;
            cartItem.Price = item.Price;
            cartItem.Total = quantity * cartItem.Price;
            
            _context.CartItems.Update(cartItem);
            _context.SaveChanges();

            return cart;
        }
    }
}
