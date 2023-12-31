﻿using CommerceClone.CustomExceptions;
using CommerceClone.DTO;
using CommerceClone.Interfaces;
using CommerceClone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommerceClone.Controllers
{
    [ApiController]
    [Route("v1/items")]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _service;

        public ItemController(IItemService service)
        {
            _service = service;
        }

        // GET: v1/items/{item_id}
        // Takes Public or Secret Key
        [HttpGet("{itemId}")]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(ItemDto))]
        public ActionResult GetItemById(int itemId)
        {
            try
            {
                string key = Request.Headers["X-Authorization"];

                ItemDto dto = _service.GetItemById(key, itemId);

                return Ok(dto);
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT: v1/items/{item_id}
        // Takes Secret Key
        [HttpPut("{itemId}")]
        [AllowAnonymous]
        [ProducesResponseType(200, Type = typeof(ItemDto))]
        public ActionResult UpdateItem(int itemId, ItemModelUpdate update)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                string key = Request.Headers["X-Authorization"];

                ItemDto dto = _service.UpdateItem(key, itemId, update);

                return Ok(dto);
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE: v1/items/{item_id}
        // Takes Secret Key
        [HttpDelete("{itemId}")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        public ActionResult DeleteItem(int itemId)
        {
            try
            {
                string key = Request.Headers["X-Authorization"];

                bool deleted = _service.DeleteItem(key, itemId);

                if (!deleted)
                    throw new Exception("Failed to delete item");

                return Ok();
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
