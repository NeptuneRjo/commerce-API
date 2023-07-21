using CommerceClone.Interfaces;
using CommerceClone.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CommerceClone.Controllers
{
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _admin;

        public AdminController(IAdminRepository adminRepo)
        {
            _admin = adminRepo;
        }

        // POST: v1/admin
        public ActionResult CreateAdmin(Admin admin)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                admin = _admin.GenerateKeys(admin);
                admin.Password = _admin.EncryptPass(admin.Password);

                _admin.Add(admin);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: v1/admin
        [Authorize]
        public ActionResult GetAdmin()
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var admin = _admin.GetByEmail(email);

                if (admin == null)
                    return NotFound();

                return Ok(admin);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
