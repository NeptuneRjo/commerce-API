using CommerceClone.DTO;
using CommerceClone.Interfaces;
using CommerceClone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace CommerceClone.Controllers
{

    [ApiController]
    [Route("v1/cart")]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cart;

        private readonly Expression<Func<Cart, object>>[] includes = { e => e.CartItems, e => e.Store, e => e.Store.Admin };

        public CartController(ICartRepository cart)
        {
            _cart = cart;
        }

        // POST: v1/cart
        [HttpPost]
        public ActionResult CreateCart(CartModel cartModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                string key = Request.Headers["X-Authorization"];

                Cart cart = _cart.CreateByKey(key, cartModel.StoreId);

                CartDto dto = _cart.Map<CartDto>(cart);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: v1/cart/{cart_id}
        [HttpGet("{cartId}")]
        public ActionResult GetCart(int cartId)
        {
            try
            {
                string key = Request.Headers["X-Authorization"];

                Cart cart = _cart.GetByQuery(e => e.Id == cartId, includes);

                if (cart == null)
                    return NotFound();

                if (cart.Store.Admin.PublicKey != key)
                    return Unauthorized();

                // Verify the info is up-to-date
                cart = _cart.UpdateInfo(cart);
                CartDto dto = _cart.Map<CartDto>(cart);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: v1/cart/{cart_id}
        [HttpPost("{cartId}")]
        [AllowAnonymous]
        public ActionResult AddItemToCart(int cartId, [FromBody] UpdateCartModel body)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                string key = Request.Headers["X-Authorization"];

                Cart cart = _cart.GetByQuery(e => e.Id == cartId, includes);

                if (cart == null)
                    return NotFound();

                if (cart.Store.Admin.PublicKey != key)
                    return Unauthorized();

                CartItem cartItem = _cart.Map<CartItem>(body);

                cart = _cart.AddItem(cart, cartItem);
                // Update item totals and subtotal
                cart = _cart.UpdateInfo(cart);

                CartDto dto = _cart.Map<CartDto>(cart);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: v1/cart/{cart_id}
        [HttpDelete("{cartId}")]
        public ActionResult DeleteCart(int cartId) 
        {
            try
            {
                string key = Request.Headers["X-Authorization"];

                Cart cart = _cart.GetByQuery(e => e.Id == cartId, includes);

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
        [HttpDelete("{cartId}/items")]
        public ActionResult EmptyCart(int cartId)
        {
            try
            {
                string key = Request.Headers["X-Authorization"];

                Expression<Func<Cart, object>>[] includes = { e => e.CartItems, e => e.Store };
                Cart cart = _cart.GetByQuery(e => e.Id == cartId, includes);

                if (cart == null)
                    return NotFound();

                if (cart.Store.Admin.PublicKey != key)
                    return Unauthorized();

                cart = _cart.ClearItems(cart);

                CartDto dto = _cart.Map<CartDto>(cart);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: v1/cart/{cart_id}/items
        [HttpPut("{cartId}/items")]
        public ActionResult UpdateItemInCart(int cartId, [FromBody] UpdateCartModel body)
        {
            try
            {
                string key = Request.Headers["X-Authorization"];

                Expression<Func<Cart, object>>[] includes = { e => e.CartItems, e => e.Store };
                Cart cart = _cart.GetByQuery(e => e.Id == cartId, includes);

                if (cart == null)
                    return NotFound();

                if (cart.Store.Admin.PublicKey != key)
                    return Unauthorized();

                cart.CartItems = _cart.RemoveItem(
                    cart.CartItems, 
                    body.ItemId, 
                    body.Quantity
                );

                CartDto cartDto = _cart.Map<CartDto>(cart);

                return Ok(cartDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
