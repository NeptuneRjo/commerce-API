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

        public IEnumerable<Store> GetStoresByAdmin(Admin admin)
        {
            return _context.Stores
                .Where(e => e.Admin == admin)
                .ToList();
        }

        public IEnumerable<Store> GetStoresByAdmin(string username)
        {
            return _context.Stores
                .Where(e => e.Admin.Username == username)
                .ToList();
        }
    }
}
