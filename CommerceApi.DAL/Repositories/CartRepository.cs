using CommerceApi.DAL.Data;
using CommerceApi.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommerceApi.DAL.Repositories
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {

        private readonly DataContext _context;

        public CartRepository(DataContext context) : base(context)
        {
            _context = context;
        }

    }
}
