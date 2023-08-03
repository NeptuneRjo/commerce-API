using CommerceClone.Models;

namespace CommerceClone.Interfaces
{
    public interface IAuthenticationRepository
    {
        string HashUserPassword(string password);
    }
}
