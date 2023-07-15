using CommerceClone.Models;
using CommerceClone.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CommerceClone.Controllers
{
    public class StoreController : Controller
    {
        private readonly StoreRepository _repository;

        public StoreController(StoreRepository storeRepository)
        {
            _repository = storeRepository;
        }

        [HttpPost]
        public void Create(Store store)
        {
            _repository.Add(store);
        }

        [HttpGet]
        public ICollection<Store> AllStores()
        {
            return _repository.GetAll();
        }

        [HttpGet]
        public Store Store(int id)
        {
            return _repository.GetById(id);
        }

        [HttpPatch]
        public void Update(Store store)
        {
            _repository.Update(store);
        }

        [HttpDelete]
        public void Delete(int id) 
        { 
            _repository.Delete(id);
        }
    }
}
