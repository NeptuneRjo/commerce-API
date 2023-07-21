using CommerceClone.Models;

namespace CommerceClone.Interfaces
{
    public interface IItemRepository : IRepository<Item>
    {
        Item GetItemByName(string name);
        ICollection<Item> GetByStore(int storeId, string pk);
        void AddToStore(string sk, int storeId, Item item);
    }
}
