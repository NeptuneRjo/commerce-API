using CommerceClone.Models;
using CommerceClone.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CommerceClone.Controllers
{
    public class CartController : Controller
    {
        private readonly CartRepository _repository;

        public CartController(CartRepository cartRepository)
        {
            _repository = cartRepository;
        }

        [HttpPost]
        public void Create(Cart cart)
        {
            _repository.Add(cart);
        }

        [HttpGet]
        public Cart GetCart(string query)
        {
            if (Int32.TryParse(query, out int i)) {
                return _repository.GetById(i);
            }

            return _repository.GetCartByUser(query);
        }

        [HttpPatch]
        public void UpdateCart(Cart cart)
        {
            _repository.Update(cart);
        }

        [HttpDelete]
        public void DeleteCart(int id) 
        { 
            _repository.Delete(id);
        }
    }
}
