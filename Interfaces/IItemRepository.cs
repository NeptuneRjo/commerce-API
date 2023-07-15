using CommerceClone.Models;

namespace CommerceClone.Interfaces
{
    public interface IItemRepository
    {
        Item GetItemByName(string name);
    }
}
