using CommerceClone.Models;

namespace CommerceClone.Interfaces
{
    public interface IAdminRepository
    {
        Admin GetAdminByUsername(string username);
    }
}
