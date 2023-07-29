using CommerceClone.Models;

namespace CommerceClone.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        Cart AddItem(Cart cart, int itemId, int quantity);
        Cart ClearItems(Cart cart);
        Cart CreateByKey(string pk, int storeId);
        ICollection<CartItem> RemoveItem(ICollection<CartItem> items, int itemId, int quantity);
    }
}
