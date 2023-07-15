using CommerceClone.Models;

namespace CommerceClone.Interfaces
{
    public interface IItemRepository
    {
        ICollection<Item> GetItemByName(string name);
    }
}
