using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CommerceClone.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [JsonPropertyName("stores")]
        public ICollection<Store>? Stores{ get; set; }

        [JsonPropertyName("secret_key")]
        public string SecretKey { get; set; } = "";

        [JsonPropertyName("public_key")]
        public string PublicKey { get; set; } = "";
    }

    public class AdminModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public void Deconstruct(out string email, out string password)
        {
            email = Email;
            password = Password;
        }
    }

    public class UpdateAdmin
    {
        public string Email { get; set; }
        public string Password { get; set; }
        [JsonPropertyName("update_password")]
        public string UpdatePassword { get; set; }

        public void Deconstruct(out string email, out string password, out string updatePassword)
        {
            email = Email;
            password = Password;
            updatePassword = UpdatePassword;
        }
    }
}
