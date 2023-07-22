using CommerceClone.Interfaces;
using CommerceClone.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace CommerceClone.Controllers
{
    [ApiController]
    [Route("v1/stores")]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cart;

        public CartController(ICartRepository cart)
        {
            _cart = cart;
        }

        // POST: v1/stores/{store_id}/cart
        [HttpPost("/{storeId}/cart")]
        public ActionResult CreateCart(int storeId, Cart cart)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                _cart.AddToStore(storeId, cart);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: v1/stores/{store_id}/cart/{cart_id}
        [HttpGet("/{storeId}/cart/{cartId}")]
        public ActionResult GetCart(int storeId, string cartId)
        {
            try
            {
                var cart = _cart.GetByUid(cartId);

                if (cart == null)
                    return NotFound();

                if (cart.StoreId != storeId)
                    return BadRequest();

                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: v1/stores/{store_id}/cart/{cart_id}
        [HttpPost("/{storeId}/cart/{cartId}")]
        public ActionResult AddItemToCart(int storeId, int cartId, [FromBody] int itemId)
        {
            try
            {
                var cart = _cart.GetById(cartId);

                if (cart == null)
                    return NotFound();

                if (cart.StoreId != storeId)
                    return BadRequest();

                cart = _cart.AddItem(cart, itemId);

                _cart.Update(cart.Id, cart);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: v1/stores/{store_id}/cart/{cart_id}
        [HttpPut("/{storeId}/cart/{cartId}")]
        public ActionResult UpdateCart(int storeId, int cartId, Cart update)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var cart = _cart.GetById(cartId);

                if (cart == null) 
                    return NotFound();

                if (cart.StoreId != storeId)
                    return BadRequest();

                _cart.Update(cartId, update);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: v1/stores/{store_id}/cart/{cart_id}
        [HttpDelete("/{storeId}/cart/{cartId}")]
        public ActionResult DeleteCart(int storeId, int cartId) 
        {
            try
            {
                var cart = _cart.GetById(cartId);

                if (cart == null)
                    return NotFound();

                if (cart.StoreId != storeId)
                    return BadRequest();

                _cart.Delete(cartId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: v1/stores/{store_id}/cart/{cart_id}/items
        [HttpDelete("/{storeId}/cart/{cartId}/items")]
        public ActionResult EmptyCart(int storeId, int cartId)
        {
            try
            {
                var cart = _cart.GetById(cartId);

                if (cart == null)
                    return NotFound();

                if (cart.StoreId != storeId)
                    return BadRequest();

                cart.Items = new List<Item>();

                _cart.Update(cartId, cart);

                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: v1/stores/{store_id}/cart/{cart_id}/items/{item_id}
        [HttpDelete("/{storeId}/cart/{cartId}/items/{itemId}")]
        public ActionResult RemoveItemFromCart(int storeId, int cartId, int itemId)
        {
            try
            {
                var cart = _cart.GetById(cartId);

                if (cart == null)
                    return NotFound();

                if (cart.StoreId != storeId)
                    return BadRequest();

                _cart.RemoveItem(cart, itemId);
                _cart.Update(cartId, cart);

                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
