using System.ComponentModel.DataAnnotations;

namespace CommerceClone.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Email { get; set; }

        public string? Password { get; set; }

        public DateTime CreatedAt { get; set; }

        public Cart Cart { get; set; }

        public WishList WishList { get; set; }

        public bool IsAdmin { get; set; }
    }
}
