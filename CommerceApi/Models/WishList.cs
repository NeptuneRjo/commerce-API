using System.ComponentModel.DataAnnotations;

namespace CommerceClone.Models
{
    public class WishList
    {
        [Key]
        public int WishListId { get; set; }

        public int TotalItems { get; set; }

        public int TotalUniqueItems { get; set; }

        public decimal Subtotal { get; set; }

        public ICollection<WishListProduct> WishListProducts { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
