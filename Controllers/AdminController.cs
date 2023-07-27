using CommerceClone.Interfaces;
using CommerceClone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text.Json.Serialization;
using CommerceClone.DTO;


namespace CommerceClone.Controllers
{
    using BCrypt.Net;
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
        private readonly IAdminRepository _admin;

        public AdminController(IAdminRepository adminRepo)
        {
            _admin = adminRepo;
        }

        // POST: v1/admin
        [HttpPost]
        public ActionResult CreateAdmin(AdminModel adminModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (_admin.GetByEmail(adminModel.Email) != null)
                return BadRequest("An admin with this email already exists");

            try
            {
                Admin admin = _admin.Map<Admin>(adminModel);

                admin = _admin.GenerateKeys(admin);
                admin.Password = _admin.EncryptPass(admin.Password);

                if (admin.Stores == null)
                    admin.Stores = new List<Store>();
                
                _admin.Add(admin);

                AdminDto dto = _admin.Map<AdminDto>(admin);
                dto.Stores = _admin.Map<ICollection<StoreDto>>(dto.Stores);

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
                Admin admin = _admin.GetByEmail(email);

                if (admin == null)
                    return NotFound("No admin with these credentials found");

                AdminDto dto = _admin.Map<AdminDto>(admin);
                dto.Stores = _admin.Map<ICollection<StoreDto>>(dto.Stores);

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
                Admin admin = _admin.GetByEmail(email);

                if (admin == null)
                    return NotFound();

                if (BCrypt.Verify(body.Password, admin.Password)) 
                {
                    admin.Password = _admin.EncryptPass(body.UpdatePassword);

                    _admin.Update(admin.Id, admin);

                    AdminDto dto = _admin.Map<AdminDto>(admin);
                    dto.Stores = _admin.Map<ICollection<StoreDto>>(dto.Stores);

                    return Ok(dto);
                }

                return BadRequest("Credentials do not match");
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
                Admin admin = _admin.GetByEmail(email);

                if (admin == null)
                    return NotFound("No admin with these credentials found");

                _admin.Delete(admin.Id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
