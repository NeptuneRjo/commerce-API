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
        [HttpPost("/create")]
        [ProducesResponseType(200, Type = typeof(AdminDto))]
        public ActionResult Signup(AdminModel adminModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(AdminDto))]
        public ActionResult Login(AdminModel adminModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                AdminDto dto = _service.GetAdmin(adminModel);

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
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(AdminDto))]
        public ActionResult UpdatePassword(UpdateAdmin body) 
        { 
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                AdminDto dto = _service.UpdateAdmin(body);

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
        [HttpDelete]
        [ProducesResponseType(200)]
        public ActionResult DeleteAdmin(AdminModel adminModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                bool deleted = _service.DeleteAdmin(adminModel);

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
