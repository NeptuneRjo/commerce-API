using CommerceApi.DAL.Entities;

namespace CommerceApi.DAL.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Product AddByKey(string sk, int storeId, Product item);
        Product AddByEmail(string email, int storeId, Product item);

        Task<Product> GetProductAsync(string id);
    }
}
