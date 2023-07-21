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

        public void AddToAdmin(string key, Store store)
        {
            var admin = _context.Admins.FirstOrDefault(e => e.SecretKey == key);

            _context.Stores.Add(store);
            admin.Stores.Add(store);

            _context.SaveChanges();
        }

        public ICollection<Store> GetAllByKey(string key)
        {
            return _context.Admins
                .FirstOrDefault(e => e.PublicKey == key)
                .Stores.ToList();
        }
    }
}
