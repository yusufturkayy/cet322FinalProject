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

        public int PrepTime { get; set; }
        public int CookTime { get; set; }
        public int Servings { get; set; }

        public string ImageUrl { get; set; } = "/images/default-recipe.jpg";

        [Required]
        public required string Category { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Foreign key - Form binding'den hariç tut ama DB için required
        [Required]
        [ScaffoldColumn(false)] // Form'da gösterme
        public string UserId { get; set; } = string.Empty;

        // Navigation properties
        public ApplicationUser? User { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();

        [NotMapped]
        public double AverageRating => Ratings.Any() ? Ratings.Average(r => r.Value) : 0;
    }
}