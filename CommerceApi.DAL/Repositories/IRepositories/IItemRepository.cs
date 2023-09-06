using CommerceApi.DAL.Entities;

namespace CommerceApi.DAL.Interfaces
{
    public interface IItemRepository : IGenericRepository<Product>
    {
        Product AddByKey(string sk, int storeId, Product item);
        Product AddByEmail(string email, int storeId, Product item);
    }
}
