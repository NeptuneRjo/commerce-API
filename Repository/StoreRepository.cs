using AutoMapper;
using CommerceClone.DTO;
using CommerceClone.Interfaces;
using CommerceClone.Models;
using Microsoft.EntityFrameworkCore;

namespace CommerceClone.Repository
{
    public class StoreRepository : Repository<Store>, IStoreRepository
    {
        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public StoreRepository(IDataContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Store AddByAdmin(Admin admin, Store store)
        {
            store.Admin = admin;

            _context.Stores.Add(store);

            admin.Stores.Add(store);

            _context.SaveChanges();

            return store;
        }

        public Store AddByEmail(string email, Store store)
        {
            Admin admin = _context.Admins.First(e => e.Email == email);

            store.Admin = admin;

            _context.Stores.Add(store);
            _context.SaveChanges();

            return store;
        }

        public Store AddByKey(string sk, Store store)
        {
            var admin = _context.Admins.FirstOrDefault(e => e.SecretKey == sk);

            store.Admin = admin;

            _context.Stores.Add(store);

            admin.Stores.Add(store);

            _context.SaveChanges();

            return store;
        }

        public Store AddItem(Item item, int storeId)
        {
            Store store = _context.Stores.FirstOrDefault(e => e.Id == storeId);

            if (item.StoreId == null)
                item.StoreId = store.Id;

            item.Store = store;

            _context.Items.Add(item);

            store.Items.Add(item);

            _context.SaveChanges();

            return store;
        }

        public ICollection<Store> GetAllByAdminId(int id)
        {
            return _context.Stores.Where(e => e.AdminId == id).ToList();
        }

        public ICollection<Store> GetAllByEmail(string email)
        {
            return _context.Stores.Include(e => e.Admin).Include(e => e.Carts).Where(e => e.Admin.Email == email).ToList();
        }

        public ICollection<Store> GetAllByPk(string pk)
        {
            return _context.Stores.Include(e => e.Admin).Include(e => e.Carts).Where(e => e.Admin.PublicKey == pk).ToList();
        }
    }
}
