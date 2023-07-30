using CommerceClone.Interfaces;
using CommerceClone.Models;

namespace CommerceClone.Repository
{
    using AutoMapper;
    using BCrypt.Net;
    using Microsoft.EntityFrameworkCore;

    public class AdminRepository : Repository<Admin>, IAdminRepository
    {
        private readonly IDataContext _context;
        private readonly IMapper _mapper;

        public AdminRepository(IDataContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public Admin GetByEmail(string email)
        {
            return _context.Admins.Include(e => e.Stores).First(e => e.Email == email);
        }

        public string EncryptPass(string password)
        {
            var salt = BCrypt.GenerateSalt();
            string hashedPass = BCrypt.HashPassword(password, salt);

            return hashedPass;
        }

        public Admin GetByPk(string pk)
        {
            return _context.Admins.First(e => e.PublicKey == pk);
        }

        public Admin GetBySk(string sk)
        {
            return _context.Admins.First(e => e.SecretKey == sk);
        }
    }
}
