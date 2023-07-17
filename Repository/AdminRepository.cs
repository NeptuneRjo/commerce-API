using CommerceClone.Interfaces;
using CommerceClone.Models;

namespace CommerceClone.Repository
{
    public class AdminRepository : Repository<Admin>, IAdminRepository
    {
        private readonly IDataContext _context;

        public AdminRepository(IDataContext context) : base(context)
        {
            _context = context;
        }

        public Admin GetAdminByEmail(string email)
        {
            return _context.Admins.SingleOrDefault(c => c.Email == email);
        }
    }
}
