using System.ComponentModel.DataAnnotations;

namespace CommerceApi.DAL.Entities
{
    public class Cart
    {
        [Key]
        public string CartId { get; set; }

        public int TotalItems { get; set; }

        public int TotalUniqueItems { get; set; }

        public decimal Subtotal { get; set; }

        public ICollection<CartProduct> CartProducts { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
