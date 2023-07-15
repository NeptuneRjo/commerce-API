﻿using CommerceClone.Interfaces;
using CommerceClone.Models;

namespace CommerceClone.Repository
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        private readonly IDataContext _context;

        public ItemRepository(IDataContext context) : base(context) 
        {
            _context = context;
        }

        public ICollection<Item> GetItemByName(string name)
        {
            return _context.Items
                .Where(c => c.Name == name)
                .ToList();
        }
    }
}
