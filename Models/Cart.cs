namespace CommerceClone.Models
{
    public enum SubtotalType
    {

    }

    public class Cart
    {
        public int Id { get; set; }
        public ICollection<CartItem> CartItems { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public int TotalItems { get; set; }
        public int TotalUniqueItems { get; set; }
        public string Subtotal { get; set; }

        public Cart()
        {
            //Items = new List<Item>();

            //TotalItems = Items.Count();

            //TotalUniqueItems = Items.GroupBy(e => e.Id)
            //    .Select(group => new { Id = group.Key, Count = group.Count() })
            //    .ToList()
            //    .Count();

            //Subtotal = Items.Sum(e => e.Price).ToString();
        }
    }
}
