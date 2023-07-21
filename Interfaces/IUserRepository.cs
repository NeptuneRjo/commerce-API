using CommerceClone.Models;

namespace CommerceClone.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByEmail(string email);
        string HashPassword(string password);
        bool Exists(string email);
    }
}
