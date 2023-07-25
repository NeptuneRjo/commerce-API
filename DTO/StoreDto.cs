using CommerceClone.Models;

namespace CommerceClone.DTO
{
    public class StoreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Item> Items { get; set; }
        public ICollection<Cart> Carts { get; set; }

        public StoreDto()
        {
            Items = new List<Item>();
        }
    }
}
