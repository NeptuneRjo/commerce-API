using CommerceApi.DAL.Data;
using CommerceApi.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommerceApi.DAL.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context) : base(context)
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

        public async Task<Product> GetProductAsync(string id)
        {
            Product product = await this.GetByQuery(e => e.ProductId == id, e => e.ProductReviews);

            return product;
        }
    }
}
