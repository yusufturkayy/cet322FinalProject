using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CookShare.Models
{
    public class ShoppingList
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Foreign keys
        [Required]
        public required string UserId { get; set; }

        // Navigation properties
        public required ApplicationUser User { get; set; }
        public required ICollection<ShoppingListItem> Items { get; set; } = new List<ShoppingListItem>();
    }

    public class ShoppingListItem
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Quantity { get; set; }

        [Required]
        public required string Unit { get; set; }

        public bool IsChecked { get; set; }

        // Foreign keys
        public int ShoppingListId { get; set; }

        // Navigation properties
        public required ShoppingList ShoppingList { get; set; }
    }
} 