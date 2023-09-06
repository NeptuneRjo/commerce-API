using AutoMapper;
using CommerceApi.DTO;
using CommerceApi.Interfaces;
using CommerceClone.CustomExceptions;
using CommerceClone.Interfaces;
using CommerceClone.Models;
using System.Linq.Expressions;

namespace CommerceApi.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _repository;
        private readonly IMapper _mapper;

        private Expression<Func<Cart, object>>[] includes = {
            e => e.CartItems
        };

        public CartService(ICartRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public CartDto AddItemToCart(string key, int id, UpdateCartModel model)
        {
            Cart cart = _repository.GetByQuery(e => e.Id == id, includes);

            if (cart == null)
                throw new ObjectNotFoundException($"No cart found with the id: {id}");

            //if (!_repository.PublicAuth(key, cart.Store.Admin))
            //    throw new UnauthorizedAccessException("Key is invalid");

            CartItem item = _mapper.Map<CartItem>(model);

            cart = _repository.AddItem(cart, item);
            cart = _repository.UpdateInfo(cart);

            CartDto dto = _mapper.Map<CartDto>(cart);

            return dto;
        }

        public bool DeleteCart(string key, int id)
        {
            Cart cart = _repository.GetByQuery(e => e.Id == id, includes);

            if (cart == null)
                throw new ObjectNotFoundException($"No cart found with the id: {id}");

            //if (!_repository.PublicAuth(key, cart.Store.Admin))
            //    throw new UnauthorizedAccessException("Key is invalid");

            try
            {
                _repository.Delete(cart.Id);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public CartDto EmptyCart(string key, int id)
        {
            Cart cart = _repository.GetByQuery(e => e.Id == id, includes);

            if (cart == null)
                throw new ObjectNotFoundException($"No cart found with the id: {id}");

            //if (!_repository.PublicAuth(key, cart.Store.Admin))
            //    throw new UnauthorizedAccessException("Key is invalid");

            cart = _repository.ClearItems(cart);
            cart = _repository.UpdateInfo(cart);

            CartDto dto = _mapper.Map<CartDto>(cart);

            return dto;
        }

        public CartDto GetCart(string key, int id)
        {
            Cart cart = _repository.GetByQuery(e => e.Id == id, includes);

            if (cart == null)
                throw new ObjectNotFoundException($"No cart found with the id: {id}");

            //if (!_repository.PublicAuth(key, cart.Store.Admin))
            //    throw new UnauthorizedAccessException("Key is invalid");

            // Verifiy the cart info is up to date
            cart = _repository.UpdateInfo(cart);

            CartDto dto = _mapper.Map<CartDto>(cart);

            return dto;
        }

        public CartDto UpdateItemInCart(string key, int id, UpdateCartModel model)
        {
            Cart cart = _repository.GetByQuery(e => e.Id == id, includes);

            if (cart == null)
                throw new ObjectNotFoundException($"No cart found with the id: {id}");

            //if (!_repository.PublicAuth(key, cart.Store.Admin))
            //    throw new UnauthorizedAccessException("Key is invalid");

            if (model.Quantity == 0)
                cart = _repository.RemoveItem(id, model.ItemId);
            else
                cart = _repository.UpdateItem(id, model.ItemId, model.Quantity);

            cart = _repository.UpdateInfo(cart);

            CartDto dto = _mapper.Map<CartDto>(cart);

            return dto;
        }
    }
}
