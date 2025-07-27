using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeShare.Application.DTOs
{
    public class RecipeDto
    {
        public Guid Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Ingredients { get; set; }
        [Required]
        public string? Steps { get; set; }
        [Range(1, int.MaxValue)]
        public int? CookingTimeInMinutes { get; set; }
        [Required]
        public string? DietaryTags { get; set; }
    }
}
