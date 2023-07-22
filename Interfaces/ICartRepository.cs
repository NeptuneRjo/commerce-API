using CommerceClone.Models;

namespace CommerceClone.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        Cart GetByUid(string uid);
        void AddToStore(int storeId, Cart cart);
        Cart AddItem(Cart cart, int itemId);
        Cart RemoveItem(Cart cart, int itemId);
    }
}
