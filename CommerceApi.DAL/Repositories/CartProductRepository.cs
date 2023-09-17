using CommerceApi.DAL.Data;
using CommerceApi.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommerceApi.DAL.Repositories
{
    public class CartProductRepository : GenericRepository<CartProduct>, ICartProductRepository
    {
        private readonly DataContext _context;

        public CartProductRepository(DataContext context) : base(context)
        {
            _context = context;
        }

    }
}
