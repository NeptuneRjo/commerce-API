using CommerceClone.Models;

namespace CommerceClone.Interfaces
{
    public interface IUserRepository
    {
        User GetUserByUsername(string username);
    }
}
