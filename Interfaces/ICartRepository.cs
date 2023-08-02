using CommerceClone.Models;

namespace CommerceClone.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        Cart AddItem(Cart cart, CartItem cartItem);
        Cart ClearItems(Cart cart);
        Cart RemoveItem(int cartId, int itemId);
        Cart UpdateInfo(Cart cart);
        Cart UpdateItem(int cartId, int itemId, int quantity);
    }
}
