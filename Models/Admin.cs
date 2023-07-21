namespace CommerceClone.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string SecretKey { get; set; }
        public string PublicKey { get; set; }

        public ICollection<Store> Stores { get; set; }

        public Admin()
        {
            Stores = new List<Store>();
        }
    }
}
