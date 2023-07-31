using CommerceClone.Interfaces;
using CommerceClone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text.Json.Serialization;
using CommerceClone.DTO;


namespace CommerceClone.Controllers
{
    public class UpdatePasswordBody
    {
        public string Password { get; set; }
        [JsonPropertyName("update_password")]
        public string UpdatePassword { get; set; }
    }

    [ApiController]
    [Route("v1/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _service;

        public AdminController(IAdminService service)
        {
            _service = service;
        }

        // POST: v1/admin
        [HttpPost]
        public ActionResult CreateAdmin(AdminModel adminModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                AdminDto dto = _service.CreateAdmin(adminModel);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: v1/admin
        [Authorize]
        [HttpGet]
        public ActionResult GetAdmin()
        {
            try
            {
                string email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                AdminDto dto = _service.GetAdmin(email);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: v1/admin
        [Authorize]
        [HttpPut]
        public ActionResult UpdatePassword([FromBody] UpdatePasswordBody body) 
        { 
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                string email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                AdminDto dto = _service.UpdateAdmin(email, body.Password, body.UpdatePassword);

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: v1/admin
        [Authorize]
        [HttpDelete]
        public ActionResult DeleteAdmin()
        {
            try
            {
                string email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                bool deleted = _service.DeleteAdmin(email);

                if (!deleted)
                    return BadRequest("Failed to delete admin");

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
