using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CookShare.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        public required string ProfilePicture { get; set; } = "/images/default-profile.png";

        [Required]
        public required string Bio { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public required ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
        public required ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public required ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public required ICollection<MealPlan> MealPlans { get; set; } = new List<MealPlan>();
        public required ICollection<ShoppingList> ShoppingLists { get; set; } = new List<ShoppingList>();
    }
} 