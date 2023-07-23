using System.ComponentModel.DataAnnotations;

namespace CommerceClone.Models
{
    public class Cart
    {
        [Key]
        public string Id { get; set; }
        public int TotalItems { get; set; }
        public int TotalUniqueItems { get; set; }
        public string Subtotal { get; set; }

        public Store Store { get; set; }
        public string StoreId { get; set; }

        public ICollection<CartItem> CartItems { get; set; }

        public Cart()
        {
            CartItems = new List<CartItem>();

            TotalItems = CartItems.Count();

            TotalUniqueItems = CartItems.GroupBy(e => e.Id)
                .Select(group => new { Id = group.Key, Count = group.Count() })
                .ToList()
                .Count();

            Subtotal = CartItems.Sum(e => e.Total).ToString();
        }
    }
}
