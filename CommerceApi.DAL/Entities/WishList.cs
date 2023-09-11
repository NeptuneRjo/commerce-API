using System.ComponentModel.DataAnnotations;

namespace CommerceApi.DAL.Entities
{
    public class WishList
    {
        public int Id { get; set; }

        public string UID { get; set; }

        public int TotalItems { get; set; }

        public int TotalUniqueItems { get; set; }

        public decimal Subtotal { get; set; }

        public ICollection<WishListProduct> WishListProducts { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
