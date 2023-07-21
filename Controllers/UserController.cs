using CommerceClone.Models;
using CommerceClone.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using CommerceClone.Interfaces;

namespace CommerceClone.Controllers
{
    using BCrypt.Net;
    using Microsoft.AspNetCore.Authorization;
    using System.Security.Claims;

    [ApiController]
    [Route("v1/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _user;

        public UserController(IUserRepository repository)
        {
            _user = repository;
        }

        // POST: v1/user
        [HttpPost]
        public ActionResult CreateUser(User user)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var exists = _user.Exists(user.Email);

                if (exists)
                    return BadRequest("A user with these credentials already exists");

                user.Password = _user.HashPassword(user.Password);

                _user.Add(user);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: v1/user
        [Authorize]
        [HttpGet]
        public ActionResult GetUser()
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = _user.GetByEmail(email);

                if (user == null)
                    return NotFound();

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: v1/user
        [Authorize]
        [HttpPut]
        public ActionResult UpdateUser(User update)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = _user.GetByEmail(email);

                if (user == null) 
                    return NotFound();

                _user.Update(user.Id, update);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: v1/user
        [Authorize]
        [HttpDelete]
        public ActionResult DeleteUser()
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var user = _user.GetByEmail(email);

                if (user == null)
                    return NotFound();

                _user.Delete(user.Id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
