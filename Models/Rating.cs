using System;
using System.ComponentModel.DataAnnotations;

namespace CookShare.Models
{
    public class Rating
    {
        public int Id { get; set; }

        [Range(1, 5)]
        public int Value { get; set; }

        public string Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Foreign keys
        public int RecipeId { get; set; }

        [Required]
        public required string UserId { get; set; }

        // Navigation properties
        public Recipe Recipe { get; set; }
        public ApplicationUser User { get; set; }
    }
} 