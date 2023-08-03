using CommerceClone.Interfaces;
using CommerceClone.Models;
using AutoMapper;

namespace CommerceClone.Repository
{
    using BCrypt.Net;

    public class AdminRepository : Repository<Admin>, IAdminRepository
    {
        private readonly IDataContext _context;

        public AdminRepository(IDataContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
        }

        public Admin GenerateKeys(Admin admin)
        {
            do
            {
                admin.PublicKey = "PK_" + Guid.NewGuid().ToString("N");
            } while (_context.Admins.Any(e => e.PublicKey == admin.PublicKey));

            do
            {
                admin.SecretKey = "SK_" + Guid.NewGuid().ToString("N");
            } while (_context.Admins.Any(e => e.SecretKey == admin.SecretKey));

            return admin;
        }

        public string EncryptPass(string password)
        {
            var salt = BCrypt.GenerateSalt();
            string hashedPass = BCrypt.HashPassword(password, salt);

            return hashedPass;
        }
    }
}
