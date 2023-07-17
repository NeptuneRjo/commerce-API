using CommerceClone.Models;

namespace CommerceClone.Interfaces
{
    public interface IAdminRepository : IRepository<Admin>
    {
        Admin GetAdminByEmail(string email);
    }
}
