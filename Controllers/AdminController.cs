using CommerceClone.Models;
using CommerceClone.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CommerceClone.Controllers
{
    public class AdminController : Controller
    {
        private AdminRepository _repository;

        public AdminController(AdminRepository adminRepository)
        {
            _repository = adminRepository;
        }

        [HttpPost("~/api/admin/create")]
        public void InsertAdmin(Admin admin)
        {
            _repository.Add(admin);
        }

        [HttpGet("~/api/admin/all")]
        public ICollection<Admin> GetAllAdmins()
        {
            return _repository.GetAll();
        }

        [HttpGet("~/api/admin/{query}")]
        public Admin GetAdmin(string query)
        {
            if (Int32.TryParse(query, out int i)) {
                return _repository.GetById(i);
            }

            return _repository.GetAdminByUsername(query);
        }

        [HttpPatch("~/api/admin/update")]
        public void UpdateAdmin(Admin admin)
        {
            _repository.Update(admin);
        }

        [HttpDelete("~/api/admin/delete")]
        public void DeleteAdmin(Admin admin)
        {
            _repository.Delete(admin);
        }
    }
}
