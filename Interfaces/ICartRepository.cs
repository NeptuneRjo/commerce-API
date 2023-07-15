using CommerceClone.Models;

namespace CommerceClone.Interfaces
{
    public interface ICartRepository
    {
        Cart GetCartByUser(User user);
    }
}
