using Microsoft.AspNetCore.Identity;

namespace CommerceClone.Models
{
    public class User : IdentityUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
