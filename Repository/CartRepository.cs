using AutoMapper;
using CommerceClone.DTO;
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

        public Cart AddItem(Cart cart, CartItem cartItem)
        {
            //cartItem.Cart = cart;
            Item item = _context.Items.Find(cartItem.ItemId);


            cartItem.CartId = cart.Id;
            cartItem.Total = item.Price * cartItem.Quantity;

            _context.CartItems.Add(cartItem);

            cart.CartItems.Add(cartItem);
            
            _context.SaveChanges();

            return cart;
        }

        public Cart ClearItems(Cart cart)
        {
            cart.CartItems.Clear();

            _context.Carts.Update(cart);
            _context.SaveChanges();

            return cart;
        }

        public ICollection<CartItem> RemoveItem(ICollection<CartItem> items, int itemId, int quantity)
        {
            var item = items.First(e => e.ItemId == itemId);

            item.Quantity = item.Quantity - quantity;

            if (item.Quantity < 1)
                _context.CartItems.Remove(item);

            _context.CartItems.Update(item);
            _context.SaveChanges();

            return items;
        }

        public Cart CreateByKey(string pk, int storeId)
        {
            Admin admin = _context.Admins.FirstOrDefault(e => e.PublicKey == pk);
            Store store = _context.Stores.FirstOrDefault(e => e.Id == storeId);

            Cart cart = new Cart();
            cart.Store = store;

            _context.Carts.Add(cart);

            store.Carts.Add(cart);

            _context.SaveChanges();

            return cart;
        }
        
        public Cart UpdateInfo(Cart cart)
        {
            //foreach (var item in cart.CartItems)
            //{
            //    cart.TotalItems += item.Quantity;
                
            //    if (item.Total != null)
            //        cart.Subtotal = cart.Subtotal + (int)item.Total;
            //}

            cart.TotalItems = cart.CartItems.Sum(e => e.Quantity);
            cart.Subtotal = cart.CartItems.Sum(e => e.Total);

            cart.TotalUniqueItems = cart.CartItems.Count();

            _context.Carts.Update(cart);
            _context.SaveChanges();

            return cart;
        }
    }
}
