namespace CommerceClone.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public string Uid { get; set; }
        public int TotalItems { get; set; }
        public int TotalUniqueItems { get; set; }
        public string Subtotal { get; set; }

        public Store Store { get; set; }
        public int StoreId { get; set; }

        public ICollection<Item> Items { get; set; }

        public Cart()
        {

            Uid = "";
            Items = new List<Item>();

            TotalItems = Items.Count();

            TotalUniqueItems = Items.GroupBy(e => e.Id)
                .Select(group => new { Id = group.Key, Count = group.Count() })
                .ToList()
                .Count();

            Subtotal = Items.Sum(e => e.Price).ToString();
        }
    }
}
