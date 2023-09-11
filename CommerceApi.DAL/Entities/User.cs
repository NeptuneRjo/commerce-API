using System.ComponentModel.DataAnnotations;

namespace CommerceApi.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string UID { get; set; }

        [Required]
        public string Email { get; set; }

        public string? Password { get; set; }

        public DateTime CreatedAt { get; set; }

        public Cart Cart { get; set; }

        public WishList WishList { get; set; }

        public bool IsAdmin { get; set; }
    }
}
