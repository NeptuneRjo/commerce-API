using CommerceClone.Models;

namespace CommerceClone.Interfaces
{
    public interface IAdminRepository : IRepository<Admin>
    {
        Admin GetAdminByUsername(string username);
    }
}
