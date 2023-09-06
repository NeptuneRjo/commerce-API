using CommerceApi.DAL.Data;
using CommerceApi.DAL.Entities;

namespace CommerceApi.DAL.Repositories
{
    public class ItemRepository : GenericRepository<Product>, IItemRepository
    {
        private readonly DataContext _context;

        public ItemRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public Product AddByEmail(string email, int storeId, Product item)
        {
            //Store store = _context.Stores.Find(storeId);

            //item.Store = store;

            //_context.Items.Add(item);

            //store.Items.Add(item);

            //_context.SaveChanges();

            //return item;
            return new Product();
        }

        public Product AddByKey(string sk, int storeId, Product item)
        {
            //Store store = _context.Stores.Find(storeId);

            //item.Store = store;

            //_context.Items.Add(item);

            //store.Items.Add(item);

            //_context.SaveChanges();

            //return item;
            return new Product();
        }
    }
}
