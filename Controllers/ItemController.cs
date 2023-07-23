using CommerceClone.Interfaces;
using CommerceClone.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommerceClone.Controllers
{
    [ApiController]
    [Route("v1/items")]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _item;
        private readonly IStoreRepository _store;

        public ItemController(IItemRepository item, IStoreRepository store)
        {
            _item = item;
            _store = store;
        }

        // POST: v1/items
        [HttpPost]
        public ActionResult CreateItem(Item item)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var key = Request.Headers["X-Authorization"];
                var store = _store.GetById(item.StoreId);

                if (store == null)
                    return NotFound();

                if (store.Admin.SecretKey != key)
                    return Unauthorized();

                _item.Add(item);
                
                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: v1/items
        [HttpGet]
        public ActionResult GetItems([FromBody] string storeId)
        {
            try
            {
                var key = Request.Headers["X-Authorization"];
                var store = _store.GetById(storeId);

                if (store == null)
                    return NotFound();

                if (store.Admin.PublicKey != key)
                    return Unauthorized();

                var items = store.Items.ToList();

                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: v1/items/{item_id}
        [HttpGet("/{itemId}")]
        public ActionResult GetItemById(string itemId)
        {
            try
            {
                var key = Request.Headers["X-Authorization"];
                var item = _item.GetById(itemId);

                if (item == null)
                    return NotFound();

                if (item.Store.Admin.PublicKey != key)
                    return Unauthorized();

                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: v1/items/{item_id}
        [HttpPut("/{itemId}")]
        public ActionResult UpdateItem(string itemId, Item update)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var key = Request.Headers["X-Authorization"];
                var item = _item.GetById(itemId);

                if (item == null)
                    return NotFound();

                if (item.Store.Admin.SecretKey != key)
                    return Unauthorized();

                _item.Update(itemId, update);

                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: v1/items/{item_id}
        [HttpDelete("/{itemId}")]
        public ActionResult DeleteItem(string itemId)
        {
            try
            {
                var key = Request.Headers["X-Authorization"];
                var item = _item.GetById(itemId);

                if (item == null)
                    return NotFound();

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
