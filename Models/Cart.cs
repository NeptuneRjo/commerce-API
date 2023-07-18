namespace CommerceClone.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int OwnerEmail { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
