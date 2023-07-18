using System.ComponentModel.DataAnnotations;

namespace CommerceClone.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public int Count { get; set; }

        public Store Store { get; set; }
        public int StoreId { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
    }
}
