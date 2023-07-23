using CommerceClone.Models;

namespace CommerceClone.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        Cart AddItem(Cart cart, string itemId, int quantity);
        Cart ClearItems(Cart cart);
        ICollection<CartItem> RemoveItem(ICollection<CartItem> items, string itemId, int quantity);
    }
}
