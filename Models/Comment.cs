using System;
using System.ComponentModel.DataAnnotations;

namespace CookShare.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        public required string Content { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Foreign keys
        public int RecipeId { get; set; }

        [Required]
        public required string UserId { get; set; }

        // Navigation properties
        public required Recipe Recipe { get; set; }
        public required ApplicationUser User { get; set; }
    }
} 