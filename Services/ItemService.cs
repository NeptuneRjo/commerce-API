using AutoMapper;
using CommerceClone.CustomExceptions;
using CommerceClone.DTO;
using CommerceClone.Interfaces;
using CommerceClone.Models;
using System.Linq.Expressions;
using System.Reflection;

namespace CommerceClone.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _repository;
        private readonly IMapper _mapper;

        private Expression<Func<Item, object>>[] includes = { e => e.Store, e => e.Store.Admin };

        public ItemService(IItemRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public bool DeleteItem(string key, int itemId)
        {
            Item item = _repository.GetByQuery(e => e.Id == itemId, includes);

            if (item == null)
                throw new ObjectNotFoundException($"No item with the id: {itemId} was found");

            if (!_repository.PrivateAuth(key, item.Store.Admin))
                throw new UnauthorizedAccessException("Invalid credentials");

            try
            {
                _repository.Delete(item.Id);
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public ItemDto GetItemById(string key, int itemId)
        {
            Item item = _repository.GetByQuery(e => e.Id == itemId, includes);

            if (item == null)
                throw new ObjectNotFoundException($"No item with the id: {itemId} was found");

            if (!_repository.PublicAuth(key, item.Store.Admin))
                throw new UnauthorizedAccessException("Invalid credentials");

            ItemDto dto = _mapper.Map<ItemDto>(item);

            return dto;
        }

        public ItemDto UpdateItem(string key, int itemId, ItemModelUpdate update)
        {
            Item item = _repository.GetByQuery(e => e.Id == itemId, includes);

            if (item == null)
                throw new ObjectNotFoundException($"No item with the id: {itemId} was found");

            if (!_repository.PrivateAuth(key, item.Store.Admin))
                throw new UnauthorizedAccessException("Invalid credentials");

            Type modelType = update.GetType();
            PropertyInfo[] properties = modelType.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name;
                object propertyValue = property.GetValue(update);

                Type itemtype = item.GetType();

                if (propertyValue != null)
                {
                    PropertyInfo itemProp = itemtype.GetProperty(propertyName);

                    itemProp.SetValue(item, propertyValue);
                }
            }

            _repository.Update(item.Id, item);

            ItemDto dto = _mapper.Map<ItemDto>(item);

            return dto;
        }
    }
}
