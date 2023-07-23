using CommerceClone.Models;

namespace CommerceClone.Interfaces
{
    public interface IAdminRepository : IRepository<Admin>
    {
        Admin GenerateKeys(Admin admin);
        string EncryptPass(string password);
        Admin GetByEmail(string email);
        Admin GetByPk(string pk);
        Admin GetBySk(string sk);
    }
}
