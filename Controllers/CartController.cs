using CommerceClone.DTO;
using CommerceClone.Interfaces;
using CommerceClone.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommerceClone.Controllers
{

    [ApiController]
    [Route("v1/cart")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;

        public CartController(ICartService service)
        {
            _service = service;
        }

        // GET: v1/cart/{cart_id}
        // Takes Public or Secret Key
        [HttpGet("{cartId}")]
        public ActionResult GetCart(int cartId)
        {
            try
            {
                string key = Request.Headers["X-Authorization"];

                CartDto dto = _service.GetCart(key, cartId);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: v1/cart/{cart_id}
        // Takes Public or Secret Key
        [HttpPost("{cartId}")]
        public ActionResult AddItemToCart(int cartId, [FromBody] UpdateCartModel body)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                string key = Request.Headers["X-Authorization"];

                CartDto dto = _service.AddItemToCart(key, cartId, body);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: v1/cart/{cart_id}
        // Takes Public or Secret Key
        [HttpDelete("{cartId}")]
        public ActionResult DeleteCart(int cartId) 
        {
            try
            {
                string key = Request.Headers["X-Authorization"];

                bool deleted = _service.DeleteCart(key, cartId);

                if (!deleted)
                    return BadRequest("Failed to delete cart");

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: v1/cart/{cart_id}/items
        // Takes Public or Secret Key
        [HttpDelete("{cartId}/items")]
        public ActionResult EmptyCart(int cartId)
        {
            try
            {
                string key = Request.Headers["X-Authorization"];

                CartDto dto = _service.EmptyCart(key, cartId);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: v1/cart/{cart_id}/items
        // Takes Public or Secret Key
        [HttpPut("{cartId}/items")]
        public ActionResult UpdateItemInCart(int cartId, [FromBody] UpdateCartModel body)
        {
            try
            {
                string key = Request.Headers["X-Authorization"];

                CartDto dto = _service.UpdateItemInCart(key, cartId, body);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
