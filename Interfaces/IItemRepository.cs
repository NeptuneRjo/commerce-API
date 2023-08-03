using CommerceClone.Models;

namespace CommerceClone.Interfaces
{
    public interface IItemRepository : IRepository<Item>
    {
        Item AddByKey(string sk, int storeId, Item item);
        Item AddByEmail(string email, int storeId, Item item);
    }
}
