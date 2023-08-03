using AutoMapper;
using CommerceClone.CustomExceptions;
using CommerceClone.DTO;
using CommerceClone.Interfaces;
using CommerceClone.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq.Expressions;

namespace CommerceClone.Services
{
    public class StoreService : IStoreService
    {
        private IStoreRepository _repository;
        private IMapper _mapper;

        private Expression<Func<Store, object>>[] includes = { e => e.Admin };

        public StoreService(IStoreRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ICollection<StoreDto> GetStores(string key)
        {
            Expression<Func<Store, bool>> query;
            
            string keyid = key.Substring(0, 3);

            if (keyid == "PK_")
                query = e => e.Admin.PublicKey == key;
            else if (keyid == "SK_")
                query = e => e.Admin.SecretKey == key;
            else
                throw new ArgumentException("Key is not valid");

            ICollection<Store> stores = _repository.GetAllByQuery(query, includes);
            ICollection<StoreDto> dtos = _mapper.Map<ICollection<StoreDto>>(stores);
        
            return dtos;
        }

        public StoreDto GetStoreById(string key, int storeId)
        {
            Store store = _repository.GetByQuery(e => e.Id == storeId, includes);

            if (store == null)
                throw new ObjectNotFoundException($"No store found with the id: {storeId}");

            if (!_repository.PublicAuth(key, store.Admin))
                throw new UnauthorizedAccessException("Key is invalid");

            StoreDto dto = _mapper.Map<StoreDto>(store);

            return dto;
        }

        public ItemDto AddItemToStore(string key, int storeId, ItemModel itemModel)
        {
            Store store = _repository.GetByQuery(e => e.Id == storeId, includes);

            if (store == null)
                throw new ObjectNotFoundException($"No store found with the id: {storeId}");

            if (!_repository.PrivateAuth(key, store.Admin))
                throw new UnauthorizedAccessException("Key is invalid");

            Item item = _repository.Map<Item>(itemModel);

            _repository.AddItem(item, store.Id);

            ItemDto dto = _mapper.Map<ItemDto>(item);
            
            return dto;
        }

        public StoreDto CreateNewStore(string key, StoreModel storeModel)
        {
            Store store = _repository.Map<Store>(storeModel);
            
            store = _repository.AddByKey(key, store);

            StoreDto dto = _mapper.Map<StoreDto>(store);

            return dto;
        }

        public ICollection<ItemDto> GetStoreItems(string key, int storeId)
        {
            Store store = _repository.GetByQuery(e => e.Id == storeId, includes);

            if (store == null)
                throw new ObjectNotFoundException($"No store found with the id: {storeId}");

            if (!_repository.PublicAuth(key, store.Admin))
                throw new UnauthorizedAccessException("Key is invalid");

            ICollection<Item> items = store.Items.ToList();
            ICollection<ItemDto> dtos = _mapper.Map<ICollection<ItemDto>>(items);

            return dtos;
        }

        public StoreDto UpdateStore(string key, int storeId, StoreModel storeModel)
        {
            Store store = _repository.GetByQuery(e => e.Id == storeId, includes);

            if (store == null)
                throw new ObjectNotFoundException($"No store found with the id: {storeId}");

            if (!_repository.PrivateAuth(key, store.Admin))
                throw new UnauthorizedAccessException("Key is invalid");

            if (storeModel.Name != null)
                store.Name = storeModel.Name;

            if (storeModel.Description != null)
                store.Description = storeModel.Description;

            _repository.Update(storeId, store);

            StoreDto dto = _mapper.Map<StoreDto>(store);

            return dto;
        }

        public bool DeleteStore(string key, int storeId)
        {
            Store store = _repository.GetByQuery(e => e.Id == storeId, includes);

            if (store == null)
                throw new ObjectNotFoundException($"No store found with the id: {storeId}");

            if (!_repository.PrivateAuth(key, store.Admin))
                throw new UnauthorizedAccessException("Key is invalid");

            try
            {
                _repository.Delete(store.Id);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public CartDto CreateCart(string key, int storeId)
        {
            Store store = _repository.GetByQuery(e => e.Id == storeId, includes);

            if (store == null)
                throw new ObjectNotFoundException($"No store found with the id: {storeId}");

            if (!_repository.PublicAuth(key, store.Admin))
                throw new UnauthorizedAccessException("Key is invalid");

            Cart cart = _repository.AddCart(store.Id);

            CartDto dto = _mapper.Map<CartDto>(cart);

            return dto;
        }
    }
}
