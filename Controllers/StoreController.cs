using CommerceClone.DTO;
using CommerceClone.Interfaces;
using CommerceClone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommerceClone.Controllers
{

    [ApiController]
    [Route("v1/stores")]
    [Authorize]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _service;

        public StoreController(IStoreService service)
        {
            _service = service;
        }

        // POST: v1/stores
        // Takes Secret Key
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(StoreDto))]
        [ProducesResponseType(400)]
        public ActionResult CreateStore(StoreModel storeModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                string key = Request.Headers["X-Authorization"];

                StoreDto dto = _service.CreateNewStore(key, storeModel);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: v1/stores/{store_id}/items
        // Takes Secret Key
        [HttpPost("{storeId}/items")]
        [ProducesResponseType(200, Type = typeof(ItemDto))]
        [ProducesResponseType(400)]
        public ActionResult CreateItem(int storeId, ItemModel itemModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                string key = Request.Headers["X-Authorization"];

                ItemDto dto = _service.AddItemToStore(key, storeId, itemModel);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: v1/stores/{store_id}/cart
        // Takes Public or Secret Key
        [HttpPost("{storeId}/cart")]
        [ProducesResponseType(200, Type = typeof(CartDto))]
        [ProducesResponseType(400)]
        public ActionResult CreateCart(int storeId)
        {
            try
            {
                string key = Request.Headers["X-Authorization"];

                CartDto dto = _service.CreateCart(key, storeId);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: v1/stores
        // Takes Public or Secret Key
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<StoreDto>))]
        [ProducesResponseType(400)]
        public ActionResult GetStores()
        {
            try
            {
                string key = Request.Headers["X-Authorization"];

                ICollection<StoreDto> dtos = _service.GetStores(key);

                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: v1/stores/{store_id}
        // Takes Public or Secret Key
        [HttpGet("{storeId}")]
        [ProducesResponseType(200, Type = typeof(StoreDto))]
        [ProducesResponseType(400)]
        public ActionResult GetStoreById(int storeId)
        {
            try
            {
                string key = Request.Headers["X-Authorization"];

                StoreDto dto = _service.GetStoreById(key, storeId);

                return Ok(dto);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: v1/stores/{store_id}/items
        // Takes Public or Secret Key
        [HttpGet("{storeId}/items")]
        [ProducesResponseType(200, Type = typeof(ICollection<ItemDto>))]
        [ProducesResponseType(400)]
        public ActionResult GetStoreItems(int storeId)
        {
            try
            {
                string key = Request.Headers["X-Authorization"];

                ICollection<ItemDto> dtos = _service.GetStoreItems(key, storeId);

                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT: v1/stores/{store_id}
        // Takes Secret Key
        [HttpPut("{storeId}")]
        [ProducesResponseType(200, Type = typeof(StoreDto))]
        [ProducesResponseType(400)]
        public ActionResult UpdateStore(int storeId, StoreModel storeModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                string key = Request.Headers["X-Authorization"];

                StoreDto dto = _service.UpdateStore(key, storeId, storeModel);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: v1/stores/{store_id}
        // Takes Secret Key
        [HttpDelete("{storeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult DeleteStore(int storeId)
        {
            try
            {
                string key = Request.Headers["X-Authorization"];

                bool deleted = _service.DeleteStore(key, storeId);

                if (deleted)
                    return Ok();
                else
                    return BadRequest("Failed to delete store");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
