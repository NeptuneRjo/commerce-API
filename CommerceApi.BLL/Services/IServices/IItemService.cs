using CommerceApi.DTO.DTOS;

namespace CommerceApi.BLL.Services
{
    public interface IItemService
    {
        /// <summary>
        /// Queries the database for the item that matches the id
        /// </summary>
        /// <param name="key"></param>
        /// <param name="itemId"></param>
        /// <returns>The found <see cref="ProductDto"/> object</returns>
        ProductDto GetItemById(string key, int itemId);
        /// <summary>
        /// Queries the database for the item that matches the id and updates its properties
        /// </summary>
        /// <param name="key"></param>
        /// <param name="itemId"></param>
        /// <param name="update"></param>
        /// <returns>The updated <see cref="ProductDto"/> object</returns>
        ProductDto UpdateItem(string secrretKey, int itemId);
        /// <summary>
        /// Queries the database for the item that matches the id and deletes it
        /// </summary>
        /// <param name="key"></param>
        /// <param name="itemId"></param>
        /// <returns>true if the item is successfully deleted</returns>
        bool DeleteItem(string secretKey, int itemId);
    }
}
