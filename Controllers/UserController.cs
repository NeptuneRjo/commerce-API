using CommerceClone.Models;
using CommerceClone.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CommerceClone.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository _repository;

        public UserController(UserRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public void Create(User user)
        {
            _repository.Add(user);
        }

        [HttpGet]
        public ICollection<User> GetAll()
        {
            return _repository.GetAll();
        }

        [HttpGet]
        public User GetUser(string query) 
        { 
            if (Int32.TryParse(query, out int i))
            {
                return _repository.GetById(i);
            }

            return _repository.GetUserByUsername(query);
        }

        [HttpPatch]
        public void Update(User user) 
        { 
            _repository.Update(user);
        }

        [HttpDelete]
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}
