using CommerceClone.Interfaces;
using CommerceClone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using CommerceClone.DTO;
using CommerceClone.CustomExceptions;

namespace CommerceClone.Controllers
{
    [ApiController]
    [Route("v1/admin")]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _service;

        public AdminController(IAdminService service)
        {
            _service = service;
        }

        // POST: v1/admin
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(AdminDto))]
        public ActionResult CreateAdmin(AdminModel adminModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                AdminDto dto = _service.CreateAdmin(adminModel);

                return Ok(dto);
            }
            catch (ObjectExistsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: v1/admin
        [Authorize]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(AdminDto))]
        public ActionResult GetAdmin()
        {
            try
            {
                string email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                AdminDto dto = _service.GetAdmin(email);

                return Ok(dto);
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT: v1/admin
        [Authorize]
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(AdminDto))]
        public ActionResult UpdatePassword([FromBody] UpdateAdmin body) 
        { 
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                string email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                AdminDto dto = _service.UpdateAdmin(email, body.Password, body.UpdatePassword);

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

        // DELETE: v1/admin
        [Authorize]
        [HttpDelete]
        [ProducesResponseType(200)]
        public ActionResult DeleteAdmin()
        {
            try
            {
                string email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                bool deleted = _service.DeleteAdmin(email);

                if (!deleted)
                    return StatusCode(500, "Failed to delete admin");

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
