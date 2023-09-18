using CommerceApi.BLL.Services;
using CommerceApi.BLL.Utilities;
using CommerceApi.DAL.Entities;
using CommerceApi.DTO.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace CommerceApi.API.Controllers
{

    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;

        private Expression<Func<Cart, object>>[] includes = { e => e.CartProducts };

        public CartController(ICartService service)
        {
            _service = service;
        }

        // GET: v1/cart/{cart_id}
        [HttpGet("{cartId}")]
        public async Task<IActionResult> GetCart(string cartId)
        {
            try
            {
                return Ok(await _service.GetEntityAsync<CartDto>(e => e.UID == cartId, includes));
            }
            catch (NotFoundException)
            {
                return NotFound($"Cart with the id = {cartId} was not found");
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        // DELETE: v1/cart/{cart_id}
        [HttpDelete("{cartId}"), Authorize]
        public async Task<IActionResult> DeleteCart(string cartId)
        {
            try
            {
                await _service.DeleteEntityAsync(e => e.UID == cartId);
                return Ok($"Cart with the id = {cartId} was successfully deleted");
            }
            catch (NotFoundException)
            {
                return NotFound($"Cart with the id = {cartId} was not found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{cartId}"), Authorize]
        public async Task<IActionResult> UpdateCart(string cartId, [FromBody] CartDto cartDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (cartId != cartDto.UID)
                return BadRequest("Id does not match request body");

            try
            {
                return Ok(await _service.UpdateCartAsync(cartId, cartDto));
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}
