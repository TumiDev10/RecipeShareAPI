﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeShare.Domain.Entities
{
    public class Recipe
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Ingredients { get; set; }
        public string? Steps { get; set; }
        public int CookingTimeInMinutes { get; set; }
        public string? DietaryTags { get; set; }
    }
}
