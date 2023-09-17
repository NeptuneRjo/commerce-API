using CommerceApi.DAL.Entities;
using CommerceApi.DAL.Repositories;
using CommerceApi.DTO.DTOS;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace CommerceApi.BLL.Utilities.Operations
{
    public class CartOperations : GenericOperations<Cart>, ICartOperations
    {
        private readonly ICartRepository _repository;
        private readonly ILogger<CartOperations> _logger;

        private Expression<Func<Cart, object>>[] includes = { e => e.CartProducts };

        public CartOperations(ICartRepository repository, ILogger<CartOperations> logger) : base(repository, logger) 
        {
            _repository = repository;
            _logger = logger;
        }
    }
}
