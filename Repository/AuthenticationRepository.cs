using CommerceClone.Interfaces;
using CommerceClone.Models;

namespace CommerceClone.Repository
{
    using BCrypt.Net;

    public class AuthenticationRepository : IAuthenticationRepository
    {
        public string HashUserPassword(string password)
        {
            string salt = BCrypt.GenerateSalt();
            string hashedPassword = BCrypt.HashPassword(password, salt);

            return hashedPassword;
        }
    }
}
