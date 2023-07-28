using AutoMapper;
using CommerceClone.DTO;
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
                string key = Request.Headers["X-Authorization"];
                Store store = _store.GetByQuery(e => e.Id == item.StoreId);

                if (store == null)
                    return NotFound();

                if (store.Admin.SecretKey != key)
                    return Unauthorized();

                _item.Add(item);
                
                ItemDto itemDto = _item.Map<ItemDto>(item);

                return Ok(itemDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: v1/items
        [HttpGet]
        public ActionResult GetItems([FromBody] int storeId)
        {
            try
            {
                string key = Request.Headers["X-Authorization"];
                Store store = _store.GetByQuery(e => e.Id == storeId);


                if (store == null)
                    return NotFound();

                if (store.Admin.PublicKey != key)
                    return Unauthorized();

                var items = store.Items.ToList();

                List<ItemDto> itemDtos = _item.Map<List<ItemDto>>(items);

                return Ok(itemDtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: v1/items/{item_id}
        [HttpGet("{itemId}")]
        public ActionResult GetItemById(int itemId)
        {
            try
            {
                string key = Request.Headers["X-Authorization"];
                Item item = _item.GetByQuery(e => e.Id == itemId);

                if (item == null)
                    return NotFound();

                if (item.Store.Admin.PublicKey != key)
                    return Unauthorized();

                ItemDto itemDto = _item.Map<ItemDto>(item);

                return Ok(itemDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: v1/items/{item_id}
        [HttpPut("{itemId}")]
        public ActionResult UpdateItem(int itemId, Item update)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                string key = Request.Headers["X-Authorization"];
                Item item = _item.GetByQuery(e => e.Id == itemId);

                if (item == null)
                    return NotFound();

                if (item.Store.Admin.SecretKey != key)
                    return Unauthorized();

                _item.Update(itemId, update);

                ItemDto itemDto = _item.Map<ItemDto>(item);

                return Ok(itemDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: v1/items/{item_id}
        [HttpDelete("{itemId}")]
        public ActionResult DeleteItem(int itemId)
        {
            try
            {
                var key = Request.Headers["X-Authorization"];
                var item = _item.GetByQuery(e => e.Id == itemId);

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
