﻿using System.Text.Json.Serialization;

namespace CommerceClone.Models
{
    public class Cart
    {
        public int Id { get; set; }
        [JsonPropertyName("total_items")]
        public int? TotalItems { get; set; }
        [JsonPropertyName("total_unique_items")]
        public int? TotalUniqueItems { get; set; }
        public decimal? Subtotal { get; set; }

        [JsonPropertyName("cart_items")]
        public ICollection<CartItem>? CartItems { get; set; }

        public Store Store { get; set; }
        public int StoreId { get; set; }

        public Cart()
        {
            CartItems = new List<CartItem>();

            TotalItems = CartItems.Count();

            TotalUniqueItems = CartItems.GroupBy(e => e.ItemId)
                .Select(group => new { Id = group.Key, Count = group.Count() })
                .ToList()
                .Count();

            Subtotal = CartItems.Sum(e => e.Total);
        }
    }

    public class CartModel
    {
        [JsonPropertyName("store_id")]
        public int StoreId { get; set; }
    }

    public class UpdateCartModel
    {
        [JsonPropertyName("item_id")]
        public int ItemId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
