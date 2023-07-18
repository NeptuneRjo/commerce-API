using CommerceClone.Models;

namespace CommerceClone.Interfaces
{
    public interface IStoreRepository
    {
        IEnumerable<Store> GetStoresByAdmin(string email);
    }
}
