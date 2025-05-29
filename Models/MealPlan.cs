using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CookShare.Models
{
    public class MealPlan
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Foreign keys
        [Required]
        public required string UserId { get; set; }

        // Navigation properties
        public required ApplicationUser User { get; set; }
        public required ICollection<MealPlanItem> MealPlanItems { get; set; } = new List<MealPlanItem>();
    }

    public class MealPlanItem
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public required string MealType { get; set; }

        // Foreign keys
        public int MealPlanId { get; set; }
        public int RecipeId { get; set; }

        // Navigation properties
        public required MealPlan MealPlan { get; set; }
        public required Recipe Recipe { get; set; }
    }
} 