using CommerceClone.DTO;
using CommerceClone.Models;

namespace CommerceClone.Interfaces
{
    public interface IStoreService
    {
        /// <summary>
        /// Queries the database for stores that have an admin 
        /// with the provided key
        /// </summary>
        /// <param name="key"></param>
        /// <returns>The queried <see cref="StoreDto"/> objects</returns>
        ICollection<StoreDto> GetStores(string key);
        /// <summary>
        /// Queries the database for the store that matches the id
        /// </summary>
        /// <param name="key"></param>
        /// <param name="id"></param>
        /// <returns>
        /// The queried <see cref="StoreDto"/> 
        /// if the key matches the public or secret key
        /// </returns>
        StoreDto GetStoreById(string key, int id);
        /// <summary>
        /// Queries the database for the store that matches the id
        /// and adds the item
        /// </summary>
        /// <param name="secretKey"></param>
        /// <param name="id"></param>
        /// <param name="itemModel"></param>
        /// <returns>The created <see cref="ItemDto"/> object</returns>
        ItemDto AddItemToStore(string secretKey, int id, ItemModel itemModel);
        /// <summary>
        /// Creates a new store under the admin that has the provided secret key 
        /// </summary>
        /// <param name="secretKey"></param>
        /// <param name="storeModel"></param>
        /// <returns>The created <see cref="StoreDto"/> object</returns>
        StoreDto CreateNewStore(string secretKey, StoreModel storeModel);
        /// <summary>
        /// Queries the database for the store that matches the id
        /// </summary>
        /// <param name="key"></param>
        /// <param name="id"></param>
        /// <returns>The store's <see cref="ItemDto"/> collection</returns>
        ICollection<ItemDto> GetStoreItems(string key, int id);
        /// <summary>
        /// Queries the database for a store that matches the id
        /// and updates it with <see cref="StoreModel"/>'s content
        /// </summary>
        /// <param name="secretKey"></param>
        /// <param name="id"></param>
        /// <param name="storeModel"></param>
        /// <returns>The update <see cref="StoreDto"/> object</returns>
        StoreDto UpdateStore(string secretKey, int id, StoreModel storeModel);
        /// <summary>
        /// Queries the database for the store that matches the id
        /// and deletes it
        /// </summary>
        /// <param name="secretKey"></param>
        /// <param name="id"></param>
        /// <returns>true if successfully deleted</returns>
        bool DeleteStore(string secretKey, int id);
        /// <summary>
        /// Queries the database for the cart that matches the id and creates a new cart
        /// </summary>
        /// <param name="key"></param>
        /// <param name="id"></param>
        /// <returns>The created <see cref="CartDto"/> object</returns>
        CartDto CreateCart(string key, int id);
    }
}
