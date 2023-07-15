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

        public Cart GetCartByUser(string user)
        {
            return _context.Carts.Find(user);
        }
    }
}
