using CommerceClone.Interfaces;
using CommerceClone.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommerceClone.Controllers
{
    [ApiController]
    [Route("v1/stores")]
    public class StoreController : ControllerBase
    {
        private readonly IStoreRepository _store;
        private readonly IAdminRepository _admin;

        public StoreController(IStoreRepository storeRepository, IAdminRepository admin)
        {
            _store = storeRepository;
            _admin = admin;
        }

        // POST: v1/stores
        [HttpPost]
        public ActionResult CreateStore(Store store)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var key = Request.Headers["X-Authorization"];
                var admin = _admin.GetBySk(key);

                if (admin == null)
                    return NotFound();

                store.AdminId = admin.Id;

                _store.Add(store);

                return Ok(store);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: v1/stores
        [HttpGet]
        public ActionResult GetStores()
        {
            try
            {
                var key = Request.Headers["X-Authorization"];
                var admin = _admin.GetByPk(key);

                if (admin == null)
                    return NotFound();

                var stores = admin.Stores.ToList();

                return Ok(stores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: v1/stores/{store_id}
        [HttpGet("/{storeId}")]
        public ActionResult GetStoreById(string storeId)
        {
            try
            {
                var key = Request.Headers["X-Authorization"];
                var store = _store.GetById(storeId);

                if (store == null)
                    return NotFound();

                if (store.Admin.PublicKey != key)
                    return Unauthorized();

                return Ok(store);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: v1/stores/{store_id}
        [HttpPut("/{storeId}")]
        public ActionResult UpdateStore(string storeId, Store update)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var key = Request.Headers["X-Authorization"];
                var store = _store.GetById(storeId);

                if (store == null)
                    return NotFound();

                if (store.Admin.SecretKey != key)
                    return Unauthorized();

                _store.Update(storeId, update);
               
                return Ok(store);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: v1/stores/{store_id}
        [HttpDelete("/{storeId}")]
        public ActionResult DeleteStore(string storeId)
        {
            try
            {
                var key = Request.Headers["X-Authorization"];
                var store = _store.GetById(storeId);

                if (store == null)
                    return NotFound();

                if (store.Admin.SecretKey != key)
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
