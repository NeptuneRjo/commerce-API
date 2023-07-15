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

        [HttpPost]
        public void Create(Admin admin)
        {
            _repository.Add(admin);
        }

        [HttpGet]
        public ICollection<Admin> AllAdmins()
        {
            return _repository.GetAll();
        }

        [HttpGet]
        public Admin GetAdmin(string query)
        {
            if (Int32.TryParse(query, out int i)) {
                return _repository.GetById(i);
            }

            return _repository.GetAdminByUsername(query);
        }

        [HttpPatch]
        public void UpdateAdmin(Admin admin)
        {
            _repository.Update(admin);
        }

        [HttpDelete]
        public void DeleteAdmin(int id)
        {
            _repository.Delete(id);
        }
    }
}
