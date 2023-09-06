using CommerceApi.DAL.Entities;

namespace CommerceApi.DAL.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        Cart AddItem(Cart cart);
        Cart ClearItems(Cart cart);
        Cart RemoveItem(int cartId, int itemId);
        Cart UpdateInfo(Cart cart);
        Cart UpdateItem(int cartId, int itemId, int quantity);
    }
}
