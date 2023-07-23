using CommerceClone.Interfaces;
using CommerceClone.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommerceClone.Controllers
{
    public class UpdateCartBody
    {
        public string ItemId { get; set; }
        public int Quantity { get; set; }

        public UpdateCartBody()
        {
            Quantity = 1;
        }
    }

    [ApiController]
    [Route("v1/cart")]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cart;
        private readonly IStoreRepository _store;

        public CartController(ICartRepository cart, IStoreRepository store)
        {
            _cart = cart;
            _store = store;
        }

        // POST: v1/cart/{store_id}
        [HttpPost("/{storeId}")]
        public ActionResult CreateCart(string storeId, Cart cart)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var key = Request.Headers["X-Authorization"];
                var store = _store.GetById(storeId);

                if (store == null)
                    return NotFound();

                if (store.Admin.PublicKey != key)
                    return Unauthorized();

                cart.StoreId = storeId;

                _cart.Add(cart);

                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: v1/cart/{cart_id}
        [HttpGet("/{cartId}")]
        public ActionResult GetCart(string cartId)
        {
            try
            {
                var key = Request.Headers["X-Authorization"];
                var cart = _cart.GetById(cartId);

                if (cart == null)
                    return NotFound();

                if (cart.Store.Admin.PublicKey != key)
                    return Unauthorized();

                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: v1/cart/{cart_id}
        [HttpPost("/{cartId}")]
        public ActionResult AddItemToCart(string cartId, [FromBody] UpdateCartBody body)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var key = Request.Headers["X-Authorization"];
                var cart = _cart.GetById(cartId);

                if (cart == null)
                    return NotFound();

                if (cart.Store.Admin.PublicKey != key)
                    return Unauthorized();

                cart = _cart.AddItem(cart, body.ItemId, body.Quantity);

                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: v1/cart/{cart_id}
        [HttpDelete("/{cartId}")]
        public ActionResult DeleteCart(string cartId) 
        {
            try
            {
                var key = Request.Headers["X-Authorization"];
                var cart = _cart.GetById(cartId);

                if (cart == null)
                    return NotFound();

                if (cart.Store.Admin.PublicKey != key)
                    return Unauthorized();

                _cart.Delete(cart.Id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: v1/cart/{cart_id}/items
        [HttpDelete("/{cartId}/items")]
        public ActionResult EmptyCart(string cartId)
        {
            try
            {
                var key = Request.Headers["X-Authorization"];
                var cart = _cart.GetById(cartId);

                if (cart == null)
                    return NotFound();

                if (cart.Store.Admin.PublicKey != key)
                    return Unauthorized();

                cart = _cart.ClearItems(cart);

                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: v1/cart/{cart_id}/items
        [HttpPut("/{cartId}/items")]
        public ActionResult UpdateItemInCart(string cartId, [FromBody] UpdateCartBody body)
        {
            try
            {
                var key = Request.Headers["X-Authorization"];
                var cart = _cart.GetById(cartId);

                if (cart == null)
                    return NotFound();

                if (cart.Store.Admin.PublicKey != key)
                    return Unauthorized();

                cart.CartItems = _cart.RemoveItem(
                    cart.CartItems, 
                    body.ItemId, 
                    body.Quantity
                );

                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
