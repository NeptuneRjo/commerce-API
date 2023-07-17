using CommerceClone.Interfaces;
using CommerceClone.Models;
using CommerceClone.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CommerceClone.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]/{query?}")]
    public class AdminController : ControllerBase
    {
        private IAdminRepository _repository;

        public AdminController(IAdminRepository adminRepository)
        {
            _repository = adminRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create(Admin admin)
        {
            try
            {
                _repository.Add(admin);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Admin> Get(string query)
        {
            try
            {
                if (Int32.TryParse(query, out int i))
                {
                    return Ok(_repository.GetById(i));
                }

                return Ok(_repository.GetAdminByEmail(query));   
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int query, Admin admin)
        {
            try
            {
                _repository.Update(query, admin);
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
