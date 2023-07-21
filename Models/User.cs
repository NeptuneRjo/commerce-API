namespace CommerceClone.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<Item> Cart { get; set; }

        public User()
        {
            Cart = new List<Item>();
        }
    }
}
