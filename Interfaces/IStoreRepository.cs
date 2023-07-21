using CommerceClone.Models;

namespace CommerceClone.Interfaces
{
    public interface IStoreRepository : IRepository<Store>
    {
        void AddToAdmin(string key, Store store);
        ICollection<Store> GetAllByKey(string key);
    }
}
