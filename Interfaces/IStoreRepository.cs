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
        Store AddByEmail(string email, Store store);
        Store AddByAdmin(Admin admin, Store store);
        ICollection<Store> GetAllByAdminId(int id);
        ICollection<Store> GetAllByPk(string pk);
        ICollection<Store> GetAllByEmail(string email);
        /// <summary>
        /// Store-specific method for retrieving the store with the Admin reference
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        new Store GetById(int id);
    }
}
