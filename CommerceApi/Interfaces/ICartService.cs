using CommerceApi.DTO;
using CommerceClone.Models;

namespace CommerceApi.Interfaces
{
    public interface ICartService
    {
        /// <summary>
        ///  Queries the database for the cart that matches the id
        /// </summary>
        /// <param name="key"></param>
        /// <param name="id"></param>
        /// <returns>The found <see cref="CartDto"/> object</returns>
        CartDto GetCart(string key, int cartId);
        /// <summary>
        /// Queries the database for the cart and item that matches the id
        /// and adds the item to the cart
        /// </summary>
        /// <param name="key"></param>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>The updated <see cref="CartDto"/> object</returns>
        CartDto AddItemToCart(string secretKey, int cartId, UpdateCartModel model);
        /// <summary>
        /// Queries the database for the cart that matches the id and deletes it
        /// </summary>
        /// <param name="key"></param>
        /// <param name="id"></param>
        /// <returns>true if successfully deleted</returns>
        bool DeleteCart(string secretKey, int cartId);
        /// <summary>
        /// Queries the database for the cart that matches the id 
        /// and deletes all of its <see cref="CartItem"/> objects
        /// </summary>
        /// <param name="key"></param>
        /// <param name="id"></param>
        /// <returns>The updated <see cref="CartDto"/> object</returns>
        CartDto EmptyCart(string secretKey, int cartId);
        /// <summary>
        /// Queries the database for the cart that matches the id and updates the corresponding item
        /// </summary>
        /// <param name="key"></param>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>The updated <see cref="CartDto"/> object</returns>
        CartDto UpdateItemInCart(string secretKey, int cartId, UpdateCartModel model);
    }
}
