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
    [Route("v1/stores")]
    [Authorize]
    public class StoreController : ControllerBase
    {
        private readonly IStoreRepository _store;

        // All of Store's reference/child objects for querying
        private readonly Expression<Func<Store, object>>[] includes = { e => e.Carts, e => e.Items, e => e.Admin };

        public StoreController(IStoreRepository storeRepository)
        {
            _store = storeRepository;
        }

        // POST: v1/stores
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(StoreDto))]
        [ProducesResponseType(400)]
        public ActionResult CreateStore(StoreModel storeModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                string key = Request.Headers["X-Authorization"];
                string email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                Store store = _store.Map<Store>(storeModel);

                _store.AddByKey(key, store);

                StoreDto dto = _store.Map<StoreDto>(store);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: v1/stores/{store_id}/items
        [HttpPost("{storeId}/items")]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(ItemDto))]
        [ProducesResponseType(400)]
        public ActionResult CreateItem(int storeId, ItemModel itemModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                string key = Request.Headers["X-Authorization"];
                string email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                Store store = _store.GetByQuery(e => e.Id == storeId, includes);

                if (store == null)
                    return NotFound();

                // If key is defined, check auth
                if (!string.IsNullOrEmpty(key) && store.Admin.PublicKey != key)
                    return Unauthorized();
                // If email is defined, check auth
                if (!string.IsNullOrEmpty(email) && store.Admin.Email != email)
                    return Unauthorized();

                itemModel.StoreId = storeId;
                Item item = _store.Map<Item>(itemModel);

                store = _store.AddItem(item, store);

                ItemDto dto = _store.Map<ItemDto>(item);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: v1/stores
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(ICollection<StoreDto>))]
        [ProducesResponseType(400)]
        public ActionResult GetStores()
        {
            try
            {
                string key = Request.Headers["X-Authorization"];
                string email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                Expression<Func<Store, bool>> query = string.IsNullOrEmpty(key)
                    ? e => e.Admin.Email == email
                    : e => e.Admin.PublicKey == key;

                ICollection<Store> stores = _store.GetAllByQuery(query, includes);

                ICollection<StoreDto> dtos = _store.Map<ICollection<StoreDto>>(stores);

                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: v1/stores/{store_id}
        [HttpGet("{storeId}")]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(StoreDto))]
        [ProducesResponseType(400)]
        public ActionResult GetStoreById(int storeId)
        {
            try
            {
                string key = Request.Headers["X-Authorization"];
                string email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                Store store = _store.GetByQuery(e => e.Id == storeId, includes);

                if (store == null)
                    return NotFound();

                // If key is defined, check auth
                if (!string.IsNullOrEmpty(key) && store.Admin.PublicKey != key)
                    return Unauthorized();
                // If email is defined, check auth
                if (!string.IsNullOrEmpty(email) && store.Admin.Email != email)
                    return Unauthorized();

                StoreDto dto = _store.Map<StoreDto>(store);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: v1/stores/{store_id}/items
        [HttpGet("{storeId}/items")]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(ICollection<ItemDto>))]
        [ProducesResponseType(400)]
        public ActionResult GetStoreItems(int storeId)
        {
            try
            {
                string key = Request.Headers["X-Authorization"];
                string email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                Store store = _store.GetByQuery(e => e.Id == storeId, includes);

                if (store == null)
                    return NotFound();

                // If key is defined, check auth
                if (!string.IsNullOrEmpty(key) && store.Admin.PublicKey != key)
                    return Unauthorized();
                // If email is defined, check auth
                if (!string.IsNullOrEmpty(email) && store.Admin.Email != email)
                    return Unauthorized();

                ICollection<ItemDto> dtos = _store.Map<ICollection<ItemDto>>(store.Items);

                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT: v1/stores/{store_id}
        [HttpPut("{storeId}")]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(StoreDto))]
        [ProducesResponseType(400)]
        public ActionResult UpdateStore(int storeId, StoreModel storeModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                string key = Request.Headers["X-Authorization"];
                string email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                Store store = _store.GetByQuery(e => e.Id == storeId, includes);

                if (store == null)
                    return NotFound();

                // If key is defined, check auth
                if (!string.IsNullOrEmpty(key) && store.Admin.SecretKey != key)
                    return Unauthorized();
                // If email is defined, check auth
                if (!string.IsNullOrEmpty(email) && store.Admin.Email != email)
                    return Unauthorized();

                if (storeModel.Name != null)
                    store.Name = storeModel.Name;

                if (storeModel.Description != null)
                    store.Description = storeModel.Description;

                _store.Update(storeId, store);

                StoreDto dto = _store.Map<StoreDto>(store);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: v1/stores/{store_id}
        [HttpDelete("{storeId}")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult DeleteStore(int storeId)
        {
            try
            {
                string key = Request.Headers["X-Authorization"];
                string email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                Store store = _store.GetByQuery(e => e.Id == storeId, includes);

                if (store == null)
                    return NotFound();

                // If key is defined, check auth
                if (!string.IsNullOrEmpty(key) && store.Admin.SecretKey != key)
                    return Unauthorized();
                // If email is defined, check auth
                if (!string.IsNullOrEmpty(email) && store.Admin.Email != email)
                    return Unauthorized();

                _store.Delete(storeId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
