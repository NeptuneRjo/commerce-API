using System.ComponentModel.DataAnnotations;

namespace CommerceClone.Models
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Item>? Items { get; set; }
        public ICollection<Cart>? Carts { get; set; }


        public Admin? Admin { get; set; }
        public int? AdminId { get; set; }

        public Store()
        {
            Items = new List<Item>();
            Carts = new List<Cart>();
        }
    }

    public class StoreModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
    }

}
