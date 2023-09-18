using CommerceApi.DAL.Entities;
using CommerceApi.DAL.Repositories;
using CommerceApi.DTO.DTOS;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceApi.BLL.Utilities.Operations
{
    public class CartProductOperations : GenericOperations<CartProduct>, ICartProductOperations
    {
        private readonly ICartProductRepository _repository;
        private readonly ILogger<CartProductOperations> _logger;

        public CartProductOperations(ICartProductRepository repository, ILogger<CartProductOperations> logger) : base(repository, logger)
        {
            _repository = repository;
            _logger = logger;
        }

    }
}
