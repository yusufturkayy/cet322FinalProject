using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CookShare.Models
{
    public class Recipe
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string Title { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public required string Instructions { get; set; }

        public int PrepTime { get; set; } // in minutes
        public int CookTime { get; set; } // in minutes
        public int Servings { get; set; }

        [Required]
        public required string ImageUrl { get; set; } = "/images/default-recipe.jpg";

        [Required]
        public required string Category { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Foreign keys
        [Required]
        public required string UserId { get; set; }

        // Navigation properties
        public required ApplicationUser User { get; set; }
        public required ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public required ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public required ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();

        // Computed properties
        [NotMapped]
        public double AverageRating => Ratings.Any() ? Ratings.Average(r => r.Value) : 0;
    }
} 