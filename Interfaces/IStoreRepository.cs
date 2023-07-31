using CommerceClone.DTO;
using CommerceClone.Models;

namespace CommerceClone.Interfaces
{
    public interface IStoreRepository : IRepository<Store>
    {
        /// <summary>
        /// Creates a new store using an admin's secret key
        /// </summary>
        /// <param name="sk"></param>
        /// <param name="store"></param>
        /// <returns>The store object after creation</returns>
        Store AddByKey(string sk, Store store);
        /// <summary>
        /// Queries the for the store that matches the id and adds the item
        /// </summary>
        /// <param name="item"></param>
        /// <param name="storeId"></param>
        /// <returns>The update <see cref="Store"/> object</returns>
        Store AddItem(Item item, int storeId);
    }
}
