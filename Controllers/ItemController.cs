using CommerceClone.Interfaces;
using CommerceClone.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommerceClone.Controllers
{
    [ApiController]
    [Route("v1/stores")]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _item;

        public ItemController(IItemRepository item)
        {
            _item = item;
        }

        // POST: v1/stores/{store_id}/items
        [HttpPost("/{storeId}/items")]
        public ActionResult CreateItem(int storeId, Item item) 
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var key = Request.Headers["X-Authorization"];

                _item.AddToStore(key, storeId, item);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: v1/stores/{store_id}/items
        [HttpGet("/{storeId}/items")]
        public ActionResult GetItems(int storeId)
        {
            try
            {
                var key = Request.Headers["X-Authorization"];

                var items = _item.GetByStore(storeId, key);

                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: v1/stores/{store_id}/items/{item_id}
        [HttpGet("/{storeId}/items/{itemId}")]
        public ActionResult GetItemById(int storeId, int itemId) 
        {
            try
            {
                var key = Request.Headers["X-Authorization"];
                var item = _item.GetById(itemId);

                if (item == null)
                    return NotFound();

                if (item.StoreId != storeId)
                    return BadRequest();

                if (item.Store.Admin.PublicKey != key)
                    return Unauthorized();

                return Ok(item);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: v1/stores/{store_id}/items/{item_id}
        [HttpPut("/{storeId}/items/{itemId}")]
        public ActionResult UpdateItem(int storeId, int itemId, Item update)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var key = Request.Headers["X-Authorization"];
                var item = _item.GetById(itemId);

                if (item == null)
                    return NotFound();

                if (item.StoreId != storeId)
                    return BadRequest();

                if (item.Store.Admin.SecretKey != key)
                    return Unauthorized();

                _item.Update(itemId, update);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: v1/stores/{store_id}/items/{item_id}
        [HttpDelete("/{storeId}/items/{itemId}")]
        public ActionResult DeleteItem(int storeId, int itemId)
        {
            try
            {
                var key = Request.Headers["X-Authorization"];
                var item = _item.GetById(itemId);

                if (item == null)
                    return NotFound();

                if (item.StoreId != storeId)
                    return BadRequest();

                if (item.Store.Admin.SecretKey != key)
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
