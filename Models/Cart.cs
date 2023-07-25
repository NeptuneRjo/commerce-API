﻿using System.Text.Json.Serialization;

namespace CommerceClone.Models
{
    public class Cart
    {
        public int Id { get; set; }
        [JsonPropertyName("total_items")]
        public int TotalItems { get; set; }
        [JsonPropertyName("total_unique_items")]
        public int TotalUniqueItems { get; set; }
        public string Subtotal { get; set; }

        [JsonPropertyName("cart_items")]
        public ICollection<CartItem> CartItems { get; set; }

        public Store Store { get; set; }

        public Cart()
        {
            CartItems = new List<CartItem>();

            TotalItems = CartItems.Count();

            TotalUniqueItems = CartItems.GroupBy(e => e.ItemId)
                .Select(group => new { Id = group.Key, Count = group.Count() })
                .ToList()
                .Count();

            Subtotal = CartItems.Sum(e => e.Total).ToString();
        }
    }
}
