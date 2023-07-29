using CommerceClone.DTO;
using CommerceClone.Interfaces;
using CommerceClone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Security.Claims;

namespace CommerceClone.Controllers
{
    [ApiController]
    [Route("v1/items")]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _item;

        private readonly Expression<Func<Item, object>>[] includes = { e => e.Store.Admin, e => e.Store };

        public ItemController(IItemRepository item)
        {
            _item = item;
        }

        // GET: v1/items/{item_id}
        [HttpGet("{itemId}")]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(ItemDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public ActionResult GetItemById(int itemId)
        {
            try
            {
                string key = Request.Headers["X-Authorization"];
                string email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                Item item = _item.GetByQuery(e => e.Id == itemId, includes);
                //Store store = _store.

                if (item == null)
                    return NotFound();

                if (!string.IsNullOrEmpty(key) && item.Store.Admin.PublicKey != key)
                    return Unauthorized();
                // If email is defined, check auth
                if (!string.IsNullOrEmpty(email) && item.Store.Admin.Email != email)
                    return Unauthorized();

                Console.WriteLine(item.Store.Admin.Email);

                ItemDto dto = _item.Map<ItemDto>(item);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: v1/items/{item_id}
        [HttpPut("{itemId}")]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(ItemDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public ActionResult UpdateItem(int itemId, ItemModelUpdate update)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                string key = Request.Headers["X-Authorization"];
                string email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                Item item = _item.GetByQuery(e => e.Id == itemId, includes);

                if (item == null)
                    return NotFound();

                if (!string.IsNullOrEmpty(key) && item.Store.Admin.SecretKey != key)
                    return Unauthorized();
                if (!string.IsNullOrEmpty(email) && item.Store.Admin.Email != email)
                    return Unauthorized();

                if (update.Name != null)
                    item.Name = update.Name;
                if (update.Description != null)
                    item.Description = update.Description;
                if (update.Image != null)
                    item.Image = update.Image;
                if (update.Count.HasValue)
                    item.Count = update.Count.Value;

                _item.Update(itemId, item);

                ItemDto dto = _item.Map<ItemDto>(item);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex.Message);
            }
        }

        // DELETE: v1/items/{item_id}
        [HttpDelete("{itemId}")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public ActionResult DeleteItem(int itemId)
        {
            try
            {
                string key = Request.Headers["X-Authorization"];
                string email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                Item item = _item.GetByQuery(e => e.Id == itemId, includes);

                if (item == null)
                    return NotFound();

                if (!string.IsNullOrEmpty(key) && item.Store.Admin.SecretKey != key)
                    return Unauthorized();
                if (!string.IsNullOrEmpty(email) && item.Store.Admin.Email != email)
                    return Unauthorized();

                _item.Delete(itemId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
