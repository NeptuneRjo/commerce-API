using System.ComponentModel.DataAnnotations;

namespace CommerceApi.DAL.Entities
{
    public class WishList
    {
        [Key]
        public string WishListId { get; set; }

        public int TotalItems { get; set; }

        public int TotalUniqueItems { get; set; }

        public decimal Subtotal { get; set; }

        public ICollection<WishListProduct> WishListProducts { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
