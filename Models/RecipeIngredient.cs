using System.ComponentModel.DataAnnotations;

namespace CookShare.Models
{
    public class RecipeIngredient
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Quantity { get; set; }

        [Required]
        public required string Unit { get; set; }

        // Foreign keys
        public int RecipeId { get; set; }

        // Navigation properties
        public required Recipe Recipe { get; set; }
    }
} 