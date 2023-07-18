using CommerceClone.Interfaces;
using CommerceClone.Models;

namespace CommerceClone.Repository
{
    public class StoreRepository : Repository<Store>, IStoreRepository
    {
        private readonly IDataContext _context;

        public StoreRepository(IDataContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<Store> GetStoresByAdmin(string email)
        {
            return _context.Stores
                .Where(e => e.OwnerEmail == email)
                .ToList();
        }
    }
}
