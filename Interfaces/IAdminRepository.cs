using CommerceClone.Models;

namespace CommerceClone.Interfaces
{
    public interface IAdminRepository : IRepository<Admin>
    {
        Admin GenerateKeys(Admin admin);
        string EncryptPass(string password);
        bool ValidatePass(Admin admin, string password);
    }
}
