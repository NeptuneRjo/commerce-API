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

        public Store AddByKey(string sk, Store store)
        {
            var admin = _context.Admins.First(e => e.SecretKey == sk);

            store.AdminId = admin.Id;
            admin.Stores.Add(store);

            _context.SaveChanges();

            return store;
        }
    }
}
