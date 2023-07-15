using CommerceClone.Interfaces;
using CommerceClone.Models;

namespace CommerceClone.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IDataContext _context;

        public UserRepository(IDataContext context) : base(context)
        {
            _context = context;
        }

        public User GetUserByUsername(string username)
        {
            return _context.Users.Find(username);
        }
    }
}
