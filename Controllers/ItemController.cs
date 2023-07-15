using CommerceClone.Models;
using CommerceClone.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CommerceClone.Controllers
{
    public class ItemController : Controller
    {
        private readonly ItemRepository _repository;

        public ItemController(ItemRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public void Create(Item item)
        {
            _repository.Add(item);
        }

        [HttpGet]
        public ICollection<Item> AllItems()
        {
            return _repository.GetAll();
        }

        [HttpGet]
        public Item GetItem(string query)
        {
            if (Int32.TryParse(query, out int i))
            {
                return _repository.GetById(i);
            }

            return _repository.GetItemByName(query);
        }

        [HttpPatch]
        public void Update(Item item)
        {
            _repository.Update(item);
        }

        [HttpDelete]
        public void DeleteItem(int id) 
        { 
            _repository.Delete(id);
        }
    }
}
