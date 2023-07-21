using CommerceClone.Models;
using CommerceClone.Repository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using CommerceClone.Interfaces;

namespace CommerceClone.Controllers
{
    using BCrypt.Net;

    [ApiController]
    [Route("api/[controller]/[action]/{query?}")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create(User user)
        {
            try
            {
                string salt = BCrypt.GenerateSalt();
                string hashedPassword = BCrypt.HashPassword(user.Password, salt);

                user.Password = hashedPassword;

                _repository.Add(user);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ICollection<User>> All()
        {
            try
            {
                return Ok(_repository.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<User> Get(string query) 
        {
            try
            {
                if (Int32.TryParse(query, out int i))
                {
                    return Ok(_repository.GetById(i));
                }

                return Ok(_repository.GetUserByEmail(query));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int query, User user) 
        {
            try
            {
                _repository.Update(query, user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Delete(int query)
        {
            try
            {
                _repository.Delete(query);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
