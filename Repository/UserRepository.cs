using CommerceClone.Interfaces;
using CommerceClone.Models;


namespace CommerceClone.Repository
{
    using BCrypt.Net;

    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IDataContext _context;

        public UserRepository(IDataContext context) : base(context)
        {
            _context = context;
        }

        public bool Exists(string email)
        {
            return _context.Users.Any(e => e.Email == email);
        }

        public User GetByEmail(string email)
        {
           return _context.Users.FirstOrDefault(e => e.Email == email);
        }

        public string HashPassword(string password)
        {
            var salt = BCrypt.GenerateSalt();
            var hashPass = BCrypt.HashPassword(password, salt);

            return hashPass;
        }
    }
}
