using CommerceClone.Models;

namespace CommerceClone.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByEmail(string email);
    }
}
